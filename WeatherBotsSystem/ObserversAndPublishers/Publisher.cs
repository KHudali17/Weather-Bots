using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherBots.DataRecords;
using WeatherBots.WeatherBots;

namespace WeatherBots.ObserversAndPublishers
{
    public class Publisher<T>
    {
        private readonly List<IPublisherObserver<T>> _observers = new();

        public bool AddObserver(IPublisherObserver<T> observer)
        {
            _observers.Add(observer);
            return true;
        }

        public bool RemoveObserver(IPublisherObserver<T> observer) => _observers.Remove(observer);

        public bool NotifyObservers(T data)
        {
            _observers.ForEach(observer => observer.Update(data));
            return true;
        }
    }
}
