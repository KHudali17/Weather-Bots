using AutoFixture;
using Moq;
using WeatherBots.DataRecords;
using WeatherBots.Seams;
using WeatherBots.WeatherBots;
using FluentAssertions;
using WeatherBotsSystem.Test.TestHelpers;
using AutoFixture.Xunit2;
using WeatherBots;

namespace WeatherBotsSystem.Test.WeatherBotsShould;

public class SnowBotShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IConsoleWrapper> _mockConsole;

    public SnowBotShould()
    {
        _fixture = new Fixture();
        _mockConsole = new Mock<IConsoleWrapper>();
    }

    [Fact]
    public async Task ThrowDataUnavailableExceptionWhenDataIsNull()
    {
        //Arrange 
        var sut = new SnowBot(
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
    public async Task ReturnFalseWhenTemperatureThresholdIsNull()
    {
        //Arrange
        var botSettingsWithNullTempThreshold = _fixture.Create<BotSettings>()
            with
        { TemperatureThreshold = null };

        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var sut = new SnowBot(
            botSettingsWithNullTempThreshold,
            _mockConsole.Object,
            _fixture.Create<WeatherData>());

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnFalseWhenTemperatureEqualsThreshold(float fixedTemperature)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Temperature = fixedTemperature };

        var fakeSettingsWithThresholdEqualToTemperature =
            _fixture.Create<BotSettings>()
            with
            { TemperatureThreshold = fixedTemperature };

        var sut = new SnowBot(
            fakeSettingsWithThresholdEqualToTemperature,
            _mockConsole.Object, fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnFalseWhenTemperatureExceedsThreshold(
        float fixedTemperature,
        float offset)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Temperature = fixedTemperature + offset };

        var fakeSettingsWithThresholdLowerThanTemperature =
            _fixture.Create<BotSettings>()
            with
            { TemperatureThreshold = fixedTemperature };

        var sut = new SnowBot(
            fakeSettingsWithThresholdLowerThanTemperature,
            _mockConsole.Object,
            fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnTrueWhenThresholdExceedsTemperature(
        float fixedTemperature,
        float offset)
    {
        //Arrange
        _fixture
            .Customizations
            .Add(new ValidWeatherDataAutoFixtureBuilder());

        var fakeWeatherData = _fixture.Create<WeatherData>()
            with
        { Temperature = fixedTemperature };

        var fakeSettingsWithThresholdHigherThanTemperature =
            _fixture.Create<BotSettings>()
            with
            { TemperatureThreshold = fixedTemperature + offset };

        var sut = new SnowBot(
            fakeSettingsWithThresholdHigherThanTemperature,
            _mockConsole.Object,
            fakeWeatherData);

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

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        firstCapturedMessage
            .Should().Be("SnowBot activated");

        secondCapturedMessage
            .Should()
            .Be(fakeSettingsWithThresholdHigherThanTemperature.Message);

        botActionResult.Should().BeTrue();
    }
}

