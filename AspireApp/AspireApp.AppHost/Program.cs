using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

var stateStore = builder.AddDaprStateStore("statestore");
var pubSub = builder.AddDaprPubSub("pubsub");

builder.AddProject<Projects.checkout>("checkout")
    .WithDaprSidecar() 
    .WithReference(stateStore)
    .WithReference(pubSub); ;

builder.AddProject<Projects.order_processor>("order-processor")
    .WithDaprSidecar() 
    .WithReference(stateStore)
    .WithReference(pubSub); ;

builder.Build().Run();
