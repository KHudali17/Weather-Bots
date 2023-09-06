using WeatherBots.DataAccess;
using WeatherBots.DataRecords;
using WeatherBots.ObserversAndPublishers;
using WeatherBots.Prompts;
using WeatherBots.Seams;
using WeatherBots.WeatherBots;

namespace WeatherBots;

public class Program
{
    private const string FilePathPromptMessage = "Enter filepath to weather data: ";
    private const string ConfigFileName = "Configs.json";
    static async Task Main(string[] args)
    {
        var config = await ConfigRetrieverJson.GetConfigFromJson(ConfigFileName);

        var weatherDataPublisher = new Publisher<WeatherData>();

        var consoleWrapper = new ConsoleWrapper();

        var weatherBotFactory = new WeatherBotFactory(config, consoleWrapper);

        var weatherBotService = new DynamicWeatherBotService(weatherBotFactory);
        weatherBotService.InstantiateBots();
        weatherBotService.AddBotsToPublisher(weatherDataPublisher);

        var retrieverFactory = new WeatherDataRetrieverFactory<string>();

        var fileWrapper = new FileWrapper();
        var prompter = new Prompter(consoleWrapper, fileWrapper);
        var pathToWeatherData = await prompter.PromptFilePathAsync(FilePathPromptMessage);

        var retriever = retrieverFactory.GetWeatherDataRetriever(pathToWeatherData);

        var inputData = await retriever.GetWeatherData();

        weatherDataPublisher.NotifyObservers(inputData);
    }
}