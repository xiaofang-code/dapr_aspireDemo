var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

var app = builder.Build();

app.MapDefaultEndpoints();

var cart = new List<string>();

app.MapGet("/cart", () => cart);
app.MapPost("/cart/{item}", (string item) => { cart.Add(item); return Results.Ok(cart); });

app.Run();
