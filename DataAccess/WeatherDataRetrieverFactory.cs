using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverFactory<T>
    {
        public static IWeatherDataRetriever GetWeatherDataRetriever(T source)
        {
            throw new NotImplementedException();
        }
    }
}
