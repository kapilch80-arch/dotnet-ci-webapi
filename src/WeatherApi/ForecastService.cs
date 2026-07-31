namespace WeatherApi;

/// <summary>A single day's forecast.</summary>
public record WeatherForecast(int Day, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

/// <summary>
/// Deterministic weather-forecast logic. Kept out of Program.cs so it is unit
/// tested and analyzed by SonarCloud (Program.cs is excluded as an entrypoint).
/// </summary>
public static class ForecastService
{
    private static readonly string[] Summaries =
        { "Freezing", "Cool", "Mild", "Warm", "Hot" };

    /// <summary>Map a Celsius temperature to a human-readable summary.</summary>
    public static string SummaryFor(int temperatureC)
    {
        if (temperatureC < 0)
        {
            return Summaries[0];
        }

        if (temperatureC < 10)
        {
            return Summaries[1];
        }

        if (temperatureC < 20)
        {
            return Summaries[2];
        }

        if (temperatureC < 30)
        {
            return Summaries[3];
        }

        return Summaries[4];
    }

    /// <summary>Produce a deterministic forecast for the next <paramref name="days"/> days.</summary>
    public static IReadOnlyList<WeatherForecast> Generate(int days)
    {
        if (days < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(days), "Days cannot be negative.");
        }

        var forecast = new List<WeatherForecast>(days);
        for (int day = 1; day <= days; day++)
        {
            int temperatureC = ((day * 7) % 45) - 5;
            forecast.Add(new WeatherForecast(day, temperatureC, SummaryFor(temperatureC)));
        }

        return forecast;
    }
}
