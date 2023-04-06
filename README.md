# MyCash

### How to run app as developer

At the first you need to install RabbitMQ locally or in docker. I chosed second option. So after installing RabbitMQ you need to add new user and admin permissions for him.

```
rabbitmqctl add_user test test

rabbitmqctl set_user_tags test administrator

rabbitmqctl set_permissions -p / test ".*" ".*" ".*"
