# Inveon E-Commerce Microservices

## Microservice Architecture Technologies
- API Gateway
- Identity Server
- Ocelot
- SignalR
- Iyzipay
- SMTP Mail
- RabbitMQ

## Setting Up with Docker

1. Install Docker on your machine:
    ```bash
    curl -fsSL https://get.docker.com -o get-docker.sh
    sudo sh get-docker.sh
    ```

2. Run the following commands:
    ```bash
    docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management  
    docker run -d --hostname rabbit --name rabbit rabbitmq:3-management
    ```

3. Modify the connection strings in microservices according to your needs. You can find them in the `appsettings.json` file.

## Microservice Architecture Present
![Microservice Architecture](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/inveon.png)

## Live Chat
![Live Chat](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/live_chat_signalr.gif)

## Shopping Cart
![Shopping Cart](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/shopping_cart.png)

## Order
![Order](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Order.png)

## Confirmation
![Confirmation](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Confirmation.png)

## Email
![Email](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Mail.png)

## Iyzico
![Iyzico](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/iyzico.png)
