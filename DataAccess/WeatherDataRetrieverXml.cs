using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverXml : IWeatherDataRetriever
    {
        private string _source;

        public WeatherDataRetrieverXml(string source)
        {
            _source = source;
        }

        public async Task<IWeatherData> GetWeatherData()
        {
            var weatherDataDeserialized = await XmlFileReader.ReadXmlFileAsync<WeatherData>(_source);

            if (!IsValidWeatherData(weatherDataDeserialized)) throw new XmlSchemaException();

            return weatherDataDeserialized;
        }

        private bool IsValidWeatherData(WeatherData weatherDataDeserialized)
        {
            PropertyInfo[] WeatherDataProperties = typeof(WeatherData).GetProperties();

            return WeatherDataProperties
                   .Select(property => property.GetValue(weatherDataDeserialized))
                   .All(property => property != null);
        }
    }
}
