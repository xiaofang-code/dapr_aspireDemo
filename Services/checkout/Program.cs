namespace webapidemo1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.AddServiceDefaults();
            // 增加dapr问题详细展示.
            builder.Services.AddProblemDetails();

            // Add services to the container.
            builder.Services.AddControllers() ;

            //注册dapr Client
            builder.Services.AddDaprClient() ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.MapDefaultEndpoints();
          
                app.UseSwagger();
                app.UseSwaggerUI();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
