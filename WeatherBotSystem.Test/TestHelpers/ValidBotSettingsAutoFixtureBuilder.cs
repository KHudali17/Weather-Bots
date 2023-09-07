using AutoFixture;
using AutoFixture.Kernel;
using WeatherBots.DataRecords;

public class ValidBotSettingsAutoFixtureBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is Type type && type == typeof(BotSettings))
        {
            var enabled = true;
            var message = context.Create<string>();
            float? temperatureThreshold = ShouldSetThresholdNull() ? null : context.Create<float>();
            float? humidityThreshold = temperatureThreshold == null ? context.Create<float>() : null;

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

    private bool ShouldSetThresholdNull()
    {
        var random = new Random();
        return random.Next(2) == 0;
    }
}
