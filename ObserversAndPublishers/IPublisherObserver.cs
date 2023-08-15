namespace WeatherBots.ObserversAndPublishers
{
    public interface IPublisherObserver<T>
    {
        Task Update(T data);
    }
}