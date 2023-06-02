# MyCash
The application is designed to allow the user to monitor his wallet.
This means that it allows to take care of user's financial condition, control daily expenses, as well as user's stock portfolio.

### How to run app as developer

At the first you need to install RabbitMQ locally or in docker. I chose second option. To authorization we use default passwords: guest/guest.
```
docker run -it --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management
```

Then we need to configure our mssql server. I chose also version on docker
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=str0ngP8wd" -p 9900:1433 --name mssql --hostname mssql -d mcr.microsoft.com/mssql/server:2022-latest
```
