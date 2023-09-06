using AutoFixture;
using FluentAssertions;
using WeatherBots.DataRecords;

namespace WeatherBotSystem.Test.DataRecordsShould;

public class BotSettingsShould
{
    [Theory]
    [InlineData(null, null, false)]
    [InlineData(1f, null, true)]
    [InlineData(null, 1f, true)]
    [InlineData(1f, 1f, false)]
    public void XorTemperatureAndHumidityThresholds(
        float? temperatureThresold, 
        float? humidityThreshold,
        bool expected)
    {
        //Arrange
        var fixture = new Fixture();
        
        var sut = new BotSettings()
        {
            TemperatureThreshold = temperatureThresold,
            HumidityThreshold = humidityThreshold,
            Enabled = fixture.Create<bool>(),
            Message = fixture.Create<string>()
        };

        //Assert
        sut.HasExactlyOneThreshold.Should().Be(expected);
    }
}
