using System.Runtime.InteropServices;
using WeatherBots.DataAccess;

namespace WeatherBots
{
    public class Program
    {
        static void Main(string[] args)
        {
            //read config file
            //create a publisher for weather data
            //create a publisher for configs
            //create a bot factory?
            //call weatherbotService?

            //create validation factory
            var validationFactory = new ValidationStructureRetrieverFactory();
            //create a reader factory 
            var retrieverFactory = new WeatherDataRetrieverFactory<string>();
            //prompt for weatherdata
            //pass to reader service

            //use publisher to update bots
        }
    }
}