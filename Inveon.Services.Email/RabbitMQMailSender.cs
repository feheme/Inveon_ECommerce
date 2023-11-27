using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inveon.Services.Email.Messages;
using MailKit.Net.Smtp;
using Newtonsoft.Json;

namespace Inveon.Services.Email
{
    public class RabbitMQMailSender : BackgroundService
    {
        private readonly ILogger<RabbitMQMailSender> _logger;
        private IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQMailSender(ILogger<RabbitMQMailSender> logger)
        {
            _logger = logger;
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: "checkoutqueue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception)
            {
                //log exception
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, e) =>
            {
                var body = e.Body;
                var json = Encoding.UTF8.GetString(body.ToArray());
                MailDto mailDto = JsonConvert.DeserializeObject<MailDto>(json);
                SendMessage(mailDto);
            };
            _channel.BasicConsume("checkoutqueue", false, consumer);


            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }

        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }


        public void SendMessage(MailDto mailDto)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Inveon", Constants.MAIL_SENDER_ADDRESS));

            message.To.Add(MailboxAddress.Parse(mailDto.Email));
            String email = PrepareEmailContent(mailDto);
            message.Subject = "Inveon Order Details";
            message.Body = new TextPart("plain")
            {
                Text = email
            };

            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect(Constants.HOST, 465, true);
                client.Authenticate(Constants.MAIL_SENDER_ADDRESS, Constants.MAIL_SENDER_PASSWORD);
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public string PrepareEmailContent(MailDto mailDto)
        {
            // Mask the credit card number
            string creditCardNumber = mailDto.CardNumber;
            string maskedCreditCardNumber = $"{new string('*', creditCardNumber.Length - 4)}{creditCardNumber.Substring(creditCardNumber.Length - 4)}";

            // Build the email content with improved formatting
            string emailContent = @$"Dear {mailDto.FirstName} {mailDto.LastName},

            Your order has been confirmed.
            ----------------------------------------------------------------------------------
           | Order Details:                                                           
           | - Order Total                  : {mailDto.OrderTotal}  ₺                                  
           | - Discount Total             : {mailDto.DiscountTotal} ₺                                 
           | - Order Time                  : {mailDto.PickupDateTime.ToString("MM/dd/yyyy h:mm tt")}   
           | - Your Phone Number   : {mailDto.Phone}                                           
           | - Credit Card                  : {maskedCreditCardNumber}                                 
            ----------------------------------------------------------------------------------

            Thank you for choosing us. Have a great day!";

            return emailContent;
        }
    }
}
