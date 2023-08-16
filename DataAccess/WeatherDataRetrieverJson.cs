using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverJson : IWeatherDataRetriever
    {
        private readonly string _source;

        public WeatherDataRetrieverJson(string source)
        {
            _source = source;
        }

        public async Task<IWeatherData> GetWeatherData()
        {
            var weatherDataDeserialized = await JsonFileReader.ReadJsonFileAsync<WeatherData>(_source);
            return weatherDataDeserialized;
        }
    }
}
