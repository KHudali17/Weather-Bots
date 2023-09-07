using AutoFixture;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using WeatherBots.DataAccess;
using WeatherBots.DataRecords;
using WeatherBotSystem.Test.TestHelpers;

namespace WeatherBotSystem.Test.DataAccessShould;

public class ConfigRetrieverJsonShould
{
    private readonly Fixture _fixture;

    public ConfigRetrieverJsonShould() 
    {
        _fixture = new Fixture();    
    }

    [Fact]
    public async Task ThrowJsonExceptionOnInvalidConfigs()
    {
        //Arrange
        _fixture.Customizations.Add(new InvalidBotSettingsAutoFixtureBuilder());

        var invalidBotConfig = new BotConfig() 
        { 
            RainBot = _fixture.Create<BotSettings>(), 
            SnowBot = _fixture.Create<BotSettings>(), 
            SunBot = _fixture.Create<BotSettings>() 
        };

        var jsonContent = $@"
        {{
            ""RainBot"": {{
                ""enabled"": true,
                ""humidityThreshold"": ""{invalidBotConfig.RainBot.HumidityThreshold}"",
                ""temperatureThreshold"": ""{invalidBotConfig.RainBot.TemperatureThreshold}"",
                ""message"": ""{invalidBotConfig.RainBot.Message}""
                }},
            ""SunBot"": {{
                ""enabled"": true,
                ""humidityThreshold"": ""{invalidBotConfig.SunBot.HumidityThreshold}"",
                ""temperatureThreshold"": ""{invalidBotConfig.SunBot.TemperatureThreshold}"",
                ""message"": ""{invalidBotConfig.SunBot.Message}""
            }},
            ""SnowBot"": {{
                ""enabled"": true,
                ""humidityThreshold"": ""{invalidBotConfig.SnowBot.HumidityThreshold}"",
                ""temperatureThreshold"": {invalidBotConfig.SnowBot.TemperatureThreshold}"",
                ""message"": ""{invalidBotConfig.SnowBot.Message}""
            }}
        }}";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

        var action = () => ConfigRetrieverJson.GetConfigFromJson(fakeStream);

        //Assert
        await action.Should().ThrowAsync<JsonException>();
    }

    [Fact]
    public async Task ReturnParsedJsonOnValidConfigs()
    {
        //Arrange
        _fixture.Customizations.Add(new ValidBotSettingsAutoFixtureBuilder());

        var validBotConfig = new BotConfig()
        {
            RainBot = _fixture.Create<BotSettings>(),
            SnowBot = _fixture.Create<BotSettings>(),
            SunBot = _fixture.Create<BotSettings>()
        };

        var jsonContent = $@"
        {{
            ""RainBot"": {{
                ""enabled"": true,
                {(validBotConfig.RainBot.HumidityThreshold.HasValue ? $@"""humidityThreshold"": {validBotConfig.RainBot.HumidityThreshold}," : "")}
                {(validBotConfig.RainBot.TemperatureThreshold.HasValue ? $@"""temperatureThreshold"": {validBotConfig.RainBot.TemperatureThreshold}," : "")}
                ""message"": ""{validBotConfig.RainBot.Message}""
                }},
            ""SunBot"": {{
                ""enabled"": true,
                {(validBotConfig.SunBot.HumidityThreshold.HasValue ? $@"""humidityThreshold"": {validBotConfig.SunBot.HumidityThreshold}," : "")}
                {(validBotConfig.SunBot.TemperatureThreshold.HasValue ? $@"""temperatureThreshold"": {validBotConfig.SunBot.TemperatureThreshold}," : "")}
                ""message"": ""{validBotConfig.SunBot.Message}""
            }},
            ""SnowBot"": {{
                ""enabled"": true,
                {(validBotConfig.SnowBot.HumidityThreshold.HasValue ? $@"""humidityThreshold"": {validBotConfig.SnowBot.HumidityThreshold}," : "")}
                {(validBotConfig.SnowBot.TemperatureThreshold.HasValue ? $@"""temperatureThreshold"": {validBotConfig.SnowBot.TemperatureThreshold}," : "")}
                ""message"": ""{validBotConfig.SnowBot.Message}""
            }}
        }}";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

        var result = await ConfigRetrieverJson.GetConfigFromJson(fakeStream);

        //Assert
        result.Should().BeEquivalentTo(validBotConfig);
    }
}
