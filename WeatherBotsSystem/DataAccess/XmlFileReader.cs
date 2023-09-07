using System.Xml.Serialization;

namespace WeatherBots.DataAccess;

public class XmlFileReader
{
    public static async Task<T> ReadXmlFileAsync<T>(Stream stream)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));

        Task<object> deserializeTask = Task.Run(() => xmlSerializer.Deserialize(stream))!;

        return (T)await deserializeTask;
    }
}
