var builder = DistributedApplication.CreateBuilder(args);

var pubSub = builder.AddDaprPubSub("pubsub");//���dapr pubsub

builder.AddProject<Projects.checkout>("checkout").WithDaprSidecar("checkout").WithReference(pubSub);//����dapr appid ����dapr pubsub����

builder.AddProject<Projects.order_processor>("order-processor").WithDaprSidecar("order-processor").WithReference(pubSub);//����dapr appid ����dapr pubsub����

builder.Build().Run();
