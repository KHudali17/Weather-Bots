using System.Runtime.InteropServices;
using WeatherBots.DataAccess;
using WeatherBots.ObserversAndPublishers;
using WeatherBots.DataRecords;
namespace WeatherBots
{
    public class Program
    {
        private const string FilePathPromptMessage = "Enter filepath to weather data: ";
        private const string ConfigFileName = "Configs.json";
        static async Task Main(string[] args)
        {
            var config = await ConfigRetrieverJson.GetConfigFromJson(ConfigFileName);

            var weatherDataPublisher = new Publisher<WeatherData>();

            var weatherBotService = new DynamicWeatherBotService(config, weatherDataPublisher);

            var retrieverFactory = new WeatherDataRetrieverFactory<string>();

            var pathToWeatherData = await Prompt.PromptFilePathAsync(FilePathPromptMessage);
            var retriever = retrieverFactory.GetWeatherDataRetriever(pathToWeatherData);

            var inputData = await retriever.GetWeatherData();

            weatherDataPublisher.NotifyObservers(inputData);
        }
    }
}