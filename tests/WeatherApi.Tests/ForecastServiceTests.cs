using Xunit;

namespace WeatherApi.Tests;

public class ForecastServiceTests
{
    [Theory]
    [InlineData(-5, "Freezing")]
    [InlineData(5, "Cool")]
    [InlineData(15, "Mild")]
    [InlineData(25, "Warm")]
    [InlineData(35, "Hot")]
    public void SummaryFor_ReturnsExpectedBand(int temperatureC, string expected)
    {
        Assert.Equal(expected, ForecastService.SummaryFor(temperatureC));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(30)]
    public void Generate_ReturnsRequestedCount(int days)
    {
        Assert.Equal(days, ForecastService.Generate(days).Count);
    }

    [Fact]
    public void Generate_IsDeterministic()
    {
        var first = ForecastService.Generate(7);
        var second = ForecastService.Generate(7);
        Assert.Equal(first, second);
    }

    [Fact]
    public void Generate_Negative_Throws()
    {
        Assert.Throws<System.ArgumentOutOfRangeException>(() => ForecastService.Generate(-1));
    }

    [Fact]
    public void WeatherForecast_ConvertsToFahrenheit()
    {
        var forecast = new WeatherForecast(1, 0, "Freezing");
        Assert.Equal(32, forecast.TemperatureF);
    }
}
