using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    internal static class JsonFileReader
    {
        internal static async Task<T> ReadJsonFileAsync<T>(string fileName)
        {
            using FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, 
                                                     FileShare.Read, bufferSize: 4096, useAsync: true);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return ( await JsonSerializer.DeserializeAsync<T>(stream, options) )!;
        }
    }
}
