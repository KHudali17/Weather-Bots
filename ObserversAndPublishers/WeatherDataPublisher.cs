using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.ObserversAndPublishers
{
    public class WeatherDataPublisher : IPublisher<IWeatherData>
    {
        private IEnumerable<IPublisherObserver<IWeatherData>> _observers;
        public bool AddObserver(IPublisherObserver<IWeatherData> observer)
        {
            throw new NotImplementedException();
        }

        public bool Observers(IWeatherData data)
        {
            throw new NotImplementedException();
        }

        public bool RemoveObserver(IPublisherObserver<IWeatherData> observer)
        {
            throw new NotImplementedException();
        }
    }
}
