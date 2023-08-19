using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WeatherBots.DataAccess
{
    internal class XmlFileReader
    {
        internal static async Task<T> ReadXmlFileAsync<T>(string fileName)
        {
            using FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read,
                                                     FileShare.Read, bufferSize: 4096, useAsync: true);

            var xmlSerializer = new XmlSerializer(typeof(T));

            Task<object> deserializeTask = Task.Run(() => xmlSerializer.Deserialize(stream))!;

            return (T)await deserializeTask;
        }
    }
}
