using System.Runtime.InteropServices;
using WeatherBots.DataAccess;

namespace WeatherBots
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            //read config file
            string configFileName = "testConfigs.json";
            DataRecords.BotConfig config = await ConfigRetrieverJson.GetConfigFromJson(configFileName);

            //create a publisher for weather data
            //create a publisher for configs
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