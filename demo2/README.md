# ContosoCrafts - DaprCon Demo
This sample shows a more in-depth setup for integrating Dapr into an existing application. It includes a variety of [#Infrastructure-Components], and also uses docker-compose to orchestrate every thing locally.

## Requirements
You'll need to have the follow installed to propperly run the demo.
- [Docker](https://www.docker.com/get-started)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [.NET Core SDK](https://dotnet.microsoft.com/download)

## Spinning up the environment

First, spin up the supporting [#Infrastructure-Components]. Run these commands in the demo2 root directory.

```bash
> docker-compose -f docker-compose-infra.yml up -d
```
> A temporary folder named **contoso_temp** will be created in the root directory of the project as a storage location for these components.

Next, launch the application containers and sidecars.

```bash
> docker-compose up -d
```

## What's in the box

### Application Services
Service   | Role(s) | Exposed local ports
----------|---------|----------------------------------------
[Contoso Website](src/ContosoCrafts.WebSite) | Store front Web UI | 80
[Products API](src/ContosoCrafts.ProductsApi) | Products HTTP Web API | 81
[Checkout Processor](src/ContosoCrafts.CheckoutProcessor) | Subscribes and processes pub/sub messages |

### Infrastructure Components
The following components have been configuired to support some of the various [Dapr building blocks](https://docs.dapr.io/developing-applications/building-blocks/) and logging senarios.

Component | Role(s) | Exposed local ports
----------|---------|----------------------------------------
[Redis](https://redis.io/)| State store | 6379
[RabbitMQ](https://www.rabbitmq.com/)| Message Broker | 15672
[MongoDB](https://docs.mongodb.com/)| Products database | 27017
[Zipkin](https://zipkin.io/)| Distributed tracing | 9411
[Fluent Bit](https://fluentbit.io/)| Log forwarder | 24224, 24220
[Seq](https://datalust.co/seq)  | Log Aggregator | 8191, 12201
