using AutoFixture;
using FluentAssertions;
using Moq;
using System.Text;
using WeatherBots.DataAccess;
using WeatherBots.DataRecords;
using WeatherBots.Seams;
using WeatherBotsSystem.Test.TestHelpers;

namespace WeatherBotSystem.Test.DataAccessShould;

public class WeatherDataRetrieverJsonShould
{
    private readonly Fixture _fixture;
    private readonly WeatherData _fakeWeatherData;
    private readonly Mock<IFileStreamWrapper> _mockFileStreamWrapper;

    public WeatherDataRetrieverJsonShould()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new ValidWeatherDataAutoFixtureBuilder());
        _fakeWeatherData = _fixture.Create<WeatherData>();
        _mockFileStreamWrapper = new Mock<IFileStreamWrapper>();
    }

    [Fact]
    public async Task DeserializeValidXml()
    {
        //Arrange
        var jsonContent =
            $@"
            {{
                ""Location"": ""{_fakeWeatherData.Location}"",
                ""Temperature"": {_fakeWeatherData.Temperature},
                ""Humidity"": {_fakeWeatherData.Humidity}
            }}
            ";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

        string? capturedFilePath = null;
        _mockFileStreamWrapper
            .Setup(x => x.GetAsyncStream(It.IsAny<string>()))
            .Callback<string>((filePath) =>
            {
                capturedFilePath = filePath;
            })
            .Returns(fakeStream);

        var fakeSource = _fixture.Create<string>();

        var sut = new WeatherDataRetrieverJson(
            fakeSource,
            _mockFileStreamWrapper.Object);

        //Act
        var result = await sut.GetWeatherData();

        //Assert
        capturedFilePath.Should().Be(fakeSource);
        result.Should().BeEquivalentTo(_fakeWeatherData);
    }
}
