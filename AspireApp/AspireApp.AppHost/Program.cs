var builder = DistributedApplication.CreateBuilder(args);

var pubSub = builder.AddDaprPubSub("pubsub");//添加dapr pubsub

builder.AddProject<Projects.checkout>("checkout").WithDaprSidecar("checkout").WithReference(pubSub);//设置dapr appid 还有dapr pubsub引用

builder.AddProject<Projects.order_processor>("order-processor").WithDaprSidecar("order-processor").WithReference(pubSub);//设置dapr appid 还有dapr pubsub引用

builder.Build().Run();
