using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;

namespace WeatherBots.ObserversAndPublishers
{
    public class ConfigDataPublisher : IPublisher<IConfigData>
    {
        private List<IPublisherObserver<IConfigData>> _observers = new();

        public bool AddObserver(IPublisherObserver<IConfigData> observer)
        {
            _observers.Add(observer);
            return true;
        }

        public bool NotifyObservers(IConfigData data)
        {
            _observers.ForEach(observer => observer.Update(data));
            return true;
        }

        public bool RemoveObserver(IPublisherObserver<IConfigData> observer) => _observers.Remove(observer);
    }
}
