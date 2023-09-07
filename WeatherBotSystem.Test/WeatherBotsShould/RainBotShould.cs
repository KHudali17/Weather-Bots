using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WeatherBots;
using WeatherBots.DataRecords;
using WeatherBots.Seams;
using WeatherBots.WeatherBots;
using WeatherBotsSystem.Test.TestHelpers;

namespace WeatherBotsSystem.Test.WeatherBotsShould;

public class RainBotShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IConsoleWrapper> _mockConsole;

    public RainBotShould()
    {
        _fixture = new Fixture();
        _mockConsole = new Mock<IConsoleWrapper>();
    }

    [Fact]
    public async Task ThrowDataUnavailableExceptionWhenDataIsNull()
    {
        //Arrange 
        var sut = new RainBot(
            _fixture.Create<BotSettings>(),
            _mockConsole.Object,
            null);

        // Act and Assert
        await sut
            .Invoking(async bot => await bot.ExecuteBotAction())
            .Should()
            .ThrowAsync<DataUnavailableException>();
    }

    [Fact]
    public async Task ReturnFalseWhenHumidityThresholdIsNull()
    {
        //Arrange
        var botSettingsWithNullHumidityThreshold = _fixture.Create<BotSettings>()
            with
        { HumidityThreshold = null };

        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var sut = new RainBot(
            botSettingsWithNullHumidityThreshold,
            _mockConsole.Object,
            _fixture.Create<WeatherData>());

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnFalseWhenThresholdExceedsHumidity(
        float fixedHumidity,
        float offset)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Humidity = fixedHumidity };

        var fakeSettingsWithThresholdHigherThanHumidity =
            _fixture.Create<BotSettings>()
            with
            { HumidityThreshold = fixedHumidity + offset };

        var sut = new RainBot(
            fakeSettingsWithThresholdHigherThanHumidity,
            _mockConsole.Object,
            fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnFalseWhenThresholdEqualsTemperature(
        float fixedHumidity)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Humidity = fixedHumidity };

        var fakeSettingsWithThresholdEqualToHumidity =
            _fixture.Create<BotSettings>()
            with
            { HumidityThreshold = fixedHumidity };

        var sut = new RainBot(
            fakeSettingsWithThresholdEqualToHumidity,
            _mockConsole.Object, fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnTrueWhenHumidityExceedsThreshold(
        float fixedHumidity,
        float offset)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Humidity = fixedHumidity + offset };

        var fakeSettingsWithThresholdLowerThanHumidity =
            _fixture.Create<BotSettings>()
            with
            { TemperatureThreshold = fixedHumidity };


        string? firstCapturedMessage = null;
        string? secondCapturedMessage = null;

        _mockConsole
            .Setup(x => x.WriteLineAsync(It.IsAny<string>()))
            .Callback<string>((message) =>
            {
                if (firstCapturedMessage == null)
                {
                    firstCapturedMessage = message;
                }
                else
                {
                    secondCapturedMessage = message;
                }
            });

        var sut = new RainBot(
            fakeSettingsWithThresholdLowerThanHumidity,
            _mockConsole.Object,
            fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        firstCapturedMessage
            .Should().Be("RainBot activated");

        secondCapturedMessage
            .Should()
            .Be(fakeSettingsWithThresholdLowerThanHumidity.Message);

        botActionResult.Should().BeTrue();
    }
}

