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
        private IEnumerable<IPublisherObserver<IConfigData>> _observers;

        public bool AddObserver(IPublisherObserver<IConfigData> observer)
        {
            throw new NotImplementedException();
        }

        public bool Observers(IConfigData data)
        {
            throw new NotImplementedException();
        }

        public bool RemoveObserver(IPublisherObserver<IConfigData> observer)
        {
            throw new NotImplementedException();
        }
    }
}
