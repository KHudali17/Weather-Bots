using System.Reflection;
using System.Xml.Schema;
using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBots.DataAccess;

public class WeatherDataRetrieverXml : IWeatherDataRetriever
{
    private readonly string _source;
    private readonly IFileStreamWrapper _fileStreamWrapper;

    public WeatherDataRetrieverXml(string source, IFileStreamWrapper fileStreamWrapper)
    {
        _source = source;
        _fileStreamWrapper = fileStreamWrapper;
    }

    public async Task<WeatherData> GetWeatherData()
    {
        var weatherDataDeserialized = await XmlFileReader.ReadXmlFileAsync<WeatherData>(
            _fileStreamWrapper.GetAsyncStream(_source));

        if (!IsValidWeatherData(weatherDataDeserialized)) throw new XmlSchemaException();

        return weatherDataDeserialized;
    }

    private static bool IsValidWeatherData(WeatherData weatherDataDeserialized)
    {
        PropertyInfo[] WeatherDataProperties = typeof(WeatherData).GetProperties();

        return WeatherDataProperties
               .Select(property => property.GetValue(weatherDataDeserialized))
               .All(property => property != null);
    }
}
