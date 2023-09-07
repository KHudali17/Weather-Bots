using AutoFixture;
using AutoFixture.Kernel;
using WeatherBots.DataRecords;

namespace WeatherBotSystem.Test.TestHelpers;

public class InvalidBotSettingsAutoFixtureBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(BotSettings))
        {
            var enabled = true;
            var message = context.Create<string>();
            var temperatureThreshold = context.Create<float>();
            var humidityThreshold = context.Create<float>();

            return new BotSettings()
            {
                Enabled = enabled,
                Message = message,
                HumidityThreshold = humidityThreshold,
                TemperatureThreshold = temperatureThreshold
            };
        }
        return new NoSpecimen();
    }
}
