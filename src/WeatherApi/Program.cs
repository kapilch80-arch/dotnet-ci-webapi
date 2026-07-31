using WeatherApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Root + health endpoints.
app.MapGet("/", () => "WeatherApi is running. Try /health or /forecast/5");
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

// Return a deterministic N-day forecast.
app.MapGet("/forecast/{days:int}", (int days) =>
{
    if (days < 0 || days > 30)
    {
        return Results.BadRequest("days must be between 0 and 30");
    }

    return Results.Ok(ForecastService.Generate(days));
});

app.Run();
