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
        private List<IPublisherObserver<IWeatherData>> _observers = new();

        public bool AddObserver(IPublisherObserver<IWeatherData> observer)
        {
            _observers.Add(observer);
            return true;
        }

        public bool NotifyObservers(IWeatherData data)
        {
            _observers.ForEach(observer => observer.Update(data));
            return true;
        }

        public bool RemoveObserver(IPublisherObserver<IWeatherData> observer) => _observers.Remove(observer);
    }
}
