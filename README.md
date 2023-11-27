# Inveon_ECommerce

## Setting Up with Docker

To run this project with Docker, follow these steps:

1. Install Docker on your machine:
   ```bash
   curl -fsSL https://get.docker.com -o get-docker.sh
   sudo sh get-docker.sh


2. Run commads:
   ```bash
   docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.12-management
   docker run -d --hostname rabbit --name rabbit rabbitmq:3-management  


### Microservice_Architecture
![Microservice_Architecture](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/inveon.png)

### Live_Chat
![Live_Chat](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/live_chat_signalr.gif)

### Shopping_Cart
![Shopping_Cart](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/shopping_cart.png)

### Order
![Order](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Order.png)

### Confirmation
![Confirmation](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Confirmation.png)

### E_Mail
![E_Mail](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/Mail.png)

### Iyzico
![Iyzico](https://github.com/feheme/Inveon_ECommerce/blob/master/github_presentation/iyzico.png)
