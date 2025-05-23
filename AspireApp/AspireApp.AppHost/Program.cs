var builder = DistributedApplication.CreateBuilder(args);

var pubSub = builder.AddDaprPubSub("pubsub");

builder.AddProject<Projects.checkout>("checkout")
    .WithDaprSidecar("checkout")
    .WithReference(pubSub);

builder.AddProject<Projects.order_processor>("order-processor")
    .WithDaprSidecar("order-processor")
    .WithReference(pubSub);

builder.AddProject<Projects.cart>("cart")
    .WithDaprSidecar("cart")
    .WithReference(pubSub);

builder.AddProject<Projects.order>("order")
    .WithDaprSidecar("order")
    .WithReference(pubSub);

builder.Build().Run();
