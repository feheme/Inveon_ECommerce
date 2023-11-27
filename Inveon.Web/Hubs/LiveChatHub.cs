using Microsoft.AspNetCore.SignalR;

namespace Inveon.Web.Hubs
{
    public class LiveChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            string timestamp = $"{DateTime.Now.ToString("HH:mm:ss")}";
            string messageWithTimestamp = $"{message}  ({timestamp})";
            await Clients.All.SendAsync("ReceiveMessage", user, messageWithTimestamp);
        }

    }
}
