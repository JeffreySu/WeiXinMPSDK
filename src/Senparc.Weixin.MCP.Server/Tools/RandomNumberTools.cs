using System.ComponentModel;
using ModelContextProtocol.Server;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class RandomNumberTools
{
    [McpServerTool]
    [Description("Generates a random number between the specified minimum and maximum values.")]
    public int GetRandomNumber(
        [Description("Minimum value (inclusive)")] int min = 0,
        [Description("Maximum value (exclusive)")] int max = 100)
    {
        return Random.Shared.Next(min, max);
    }

        [McpServerTool]
    [Description("Describes random weather in the provided city.")]
    public string GetCityWeather(
        [Description("Name of the city to return weather for")] string city)
    {
        // Read the environment variable during tool execution.
        // Alternatively, this could be read during startup and passed via IOptions dependency injection
        var weather = Environment.GetEnvironmentVariable("WEATHER_CHOICES");
        if (string.IsNullOrWhiteSpace(weather))
        {
            weather = "balmy,rainy,stormy";
        }

        var weatherChoices = weather.Split(",");
        var selectedWeatherIndex =  Random.Shared.Next(0, weatherChoices.Length);

        return $"The weather in {city} is {weatherChoices[selectedWeatherIndex]}.";
    }
}
