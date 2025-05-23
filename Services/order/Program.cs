var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Register Dapr client for publishing events
builder.Services.AddDaprClient();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/order", async (Order order, Dapr.Client.DaprClient daprClient, ILogger<Program> logger) =>
{
    if (order.Id <= 0 || string.IsNullOrWhiteSpace(order.Product))
    {
        return Results.BadRequest("Invalid order");
    }

    try
    {
        await daprClient.PublishEventAsync("pubsub", "TestPubsub", order);
        logger.LogInformation("Order {Id} published", order.Id);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to publish order {Id}", order.Id);
        return Results.StatusCode(500);
    }

    return Results.Ok(order);
});

app.Run();

record Order(int Id, string Product);
