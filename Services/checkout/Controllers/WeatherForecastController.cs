using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
namespace webapidemo1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SendEventController : ControllerBase
    {
        private readonly DaprClient _daprClient; 
 
        private readonly ILogger<SendEventController> _logger;

        public SendEventController(DaprClient daprClient,ILogger<SendEventController> logger)
        {
            _logger = logger;
            _daprClient= daprClient;
        }

        [HttpGet("SendEvent")]
        public async Task Get()
        {
            _logger.LogInformation("begin Send");

            for (int i = 1; i <= 10; i++)
            {
                var order = new Order($"content{i}");
                await _daprClient.PublishEventAsync("pubsub", "TestPubsub", order);
                Console.WriteLine("Published data: " + order);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            _logger.LogInformation("end Send");
        }


        [HttpGet("weatherforecast")]
        public async Task<WeatherForecast[]> GetWeatherAsync()
        {
            return await _daprClient.InvokeMethodAsync<WeatherForecast[]>(HttpMethod.Get, "order-processor", "weatherforecast");
        }
    }
    public record Order(string content);

    public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
