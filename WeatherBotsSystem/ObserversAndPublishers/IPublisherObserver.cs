namespace WeatherBots.ObserversAndPublishers;

public interface IPublisherObserver<T>
{
    Task<bool> Update(T data);
}