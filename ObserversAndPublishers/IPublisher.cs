using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.ObserversAndPublishers
{
    public interface IPublisher<T>
    {
        bool AddObserver(IPublisherObserver<T> observer);

        bool RemoveObserver(IPublisherObserver<T> observer);

        bool NotifyObservers(T data);
    }
}
