using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverXml : IWeatherDataRetriever
    {
        private string _source;
        private IValidationStructureRetriever _validation;

        public WeatherDataRetrieverXml(string source, IValidationStructureRetriever validation)
        {
            _source = source;
            _validation = validation;
        }

        public Task<IWeatherData> GetWeatherData()
        {
            throw new NotImplementedException();
        }
    }
}
