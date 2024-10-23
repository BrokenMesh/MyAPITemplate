using Microsoft.AspNetCore.Mvc;
using MyAPITemplate.Database;
using MyAPITemplate.Logic;

internal class Program
{
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.Configure<ApiBehaviorOptions>(option => {
            option.InvalidModelStateResponseFactory = context => {
                var result = new UnprocessableEntityObjectResult(context.ModelState);

                result.ContentTypes.Add("application/json");
                result.ContentTypes.Add("application/xml");

                return result;
            };
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        Config.LoadConfig("./config.txt");
        DBManager.StartDataBaseConnection();

        if (app.Environment.IsDevelopment()) {
            Seeder.Seed();
        }

        app.Run();
    }
}