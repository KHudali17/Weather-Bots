using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.DataAccess
{
    public interface IWeatherDataRetriever
    {
        Task<WeatherData> GetWeatherData();
    }
}
