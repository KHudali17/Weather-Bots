using AutoFixture;
using FluentAssertions;
using Moq;
using WeatherBots.DataRecords;
using WeatherBots.Seams;

namespace WeatherBotsSystem.Test.WeatherBotsShould;

public class DynamicWeatherBotShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IConsoleWrapper> _stubConsole;

    public DynamicWeatherBotShould()
    {
        _fixture = new Fixture();
        _stubConsole = new Mock<IConsoleWrapper>();
    }

    [Fact]
    public async Task UpdateDataFieldWithUpdateMethod()
    {
        //Arrange
        var fakeSettings = _fixture.Create<BotSettings>();
        var mockData = _fixture.Create<WeatherData>();

        var sut = new TestDynamicWeatherBot(
            fakeSettings,
            _stubConsole.Object,
            mockData);

        var newData = _fixture.Create<WeatherData>();


        //Act and Assert
        await sut
            .Invoking(async bot => await bot.Update(newData))
            .Should()
            .ThrowAsync<NotImplementedException>();

        sut.Data.Should().Be(newData);
    }
}

