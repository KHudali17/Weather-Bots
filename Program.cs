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

            var weatherDataPublisher = new WeatherDataPublisher();
            var configDataPublisher = new ConfigDataPublisher();

            //create a bot factory?
            //call weatherbotService?


            //create a reader factory 
            WeatherDataRetrieverFactory<string> retrieverFactory = new WeatherDataRetrieverFactory<string>();

            

            //prompt for weatherdata
            //pass to reader service

            //use publisher to update bots
        }
    }
}