using Api.Services;
using Microsoft.AspNetCore.HttpLogging;

namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<WeatherService>();
        builder.Services.AddControllers();
        builder.Services.AddHttpLogging(options =>
        {
            options.LoggingFields = 
                HttpLoggingFields.RequestPath
                | HttpLoggingFields.RequestHeaders
                | HttpLoggingFields.RequestMethod
                | HttpLoggingFields.RequestQuery
                | HttpLoggingFields.ResponseHeaders;
        });
        builder.Services.AddLogging(options =>
        {
            options.AddJsonConsole(jsonOptions =>
            {
                jsonOptions.IncludeScopes = true;
                jsonOptions.UseUtcTimestamp = true;
            });
        });

        var app = builder.Build();
        app.UseHttpLogging();
        app.MapControllers();
        app.Run();
    }
}
