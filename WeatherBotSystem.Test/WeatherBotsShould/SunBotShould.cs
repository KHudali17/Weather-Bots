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

public class SunBotShould
{
    private readonly Fixture _fixture;
    private readonly Mock<IConsoleWrapper> _mockConsole;

    public SunBotShould()
    {
        _fixture = new Fixture();
        _mockConsole = new Mock<IConsoleWrapper>();
    }

    [Fact]
    public async Task ThrowDataUnavailableExceptionWhenDataIsNull()
    {
        //Arrange 
        var sut = new SunBot(
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

        var sut = new SunBot(
            botSettingsWithNullTempThreshold,
            _mockConsole.Object,
            _fixture.Create<WeatherData>());

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnFalseWhenThresholdExceedsTemperature(
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

        var sut = new SunBot(
            fakeSettingsWithThresholdHigherThanTemperature,
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
        float fixedTemperature)
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

        var sut = new SunBot(
            fakeSettingsWithThresholdEqualToTemperature,
            _mockConsole.Object, fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        botActionResult.Should().BeFalse();
    }

    [Theory]
    [AutoData]
    public async Task ReturnTrueWhenTemperatureExceedsThreshold(
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

        var sut = new SunBot(
            fakeSettingsWithThresholdLowerThanTemperature,
            _mockConsole.Object,
            fakeWeatherData);

        //Act
        var botActionResult = await sut.ExecuteBotAction();

        //Assert
        firstCapturedMessage
            .Should().Be("SunBot activated");

        secondCapturedMessage
            .Should()
            .Be(fakeSettingsWithThresholdLowerThanTemperature.Message);

        botActionResult.Should().BeTrue();
    }
}

