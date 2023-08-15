using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverJson : IWeatherDataRetriever
    {
        private string _source;
        private IValidationStructureRetriever _validation;

        public WeatherDataRetrieverJson(string source, IValidationStructureRetriever validation)
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
