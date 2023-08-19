using System.Runtime.InteropServices;
using WeatherBots.DataAccess;
using WeatherBots.ObserversAndPublishers;

namespace WeatherBots
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string configFileName = "testConfigs.json";
            DataRecords.BotConfig config = await ConfigRetrieverJson.GetConfigFromJson(configFileName);

            var weatherDataPublisher = new Publisher<WeatherData>();

            var weatherBotService = new DynamicWeatherBotService(config, weatherDataPublisher);

            WeatherDataRetrieverFactory<string> retrieverFactory = new WeatherDataRetrieverFactory<string>();

            //prompt for weatherdata
            string pathToWeatherData = "";
            var retriever = retrieverFactory.GetWeatherDataRetriever(pathToWeatherData);

            WeatherData inputData = await retriever.GetWeatherData();

            weatherDataPublisher.NotifyObservers(inputData);
        }
    }
}