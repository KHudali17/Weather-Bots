using AutoFixture;
using AutoFixture.Kernel;
using WeatherBots.DataRecords;

namespace WeatherBotsSystem.Test.TestHelpers;

public class ValidWeatherDataAutoFixtureBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(WeatherData))
        {
            var location = context.Create<string>();
            var temperature = context.Create<float>();
            var humidity = context.Create<float>();
            return new WeatherData
            {
                Location = location,
                Temperature = temperature,
                Humidity = humidity
            };
        }
        return new NoSpecimen();
    }
}

