var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapPost("/order", (Order order) => Results.Ok(order));

app.Run();

record Order(int Id, string Product);
