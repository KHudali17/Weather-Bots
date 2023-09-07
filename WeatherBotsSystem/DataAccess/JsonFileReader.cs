using System.Text.Json;


namespace WeatherBots.DataAccess;

public static class JsonFileReader
{
    public static async Task<T> ReadJsonFileAsync<T>(Stream stream)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return (await JsonSerializer.DeserializeAsync<T>(stream, options))!;
    }
}
