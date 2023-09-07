using AutoFixture;
using FluentAssertions;
using Moq;
using System.Text;
using System.Xml.Schema;
using WeatherBots.DataAccess;
using WeatherBots.DataRecords;
using WeatherBots.Seams;
using WeatherBotsSystem.Test.TestHelpers;

namespace WeatherBotSystem.Test.DataAccessShould;

public class WeatherDataRetrieverXmlShould
{
    private readonly Fixture _fixture;
    private readonly WeatherData _fakeWeatherData;
    private readonly Mock<IFileStreamWrapper> _mockFileStreamWrapper;

    public WeatherDataRetrieverXmlShould()
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
        var xmlContent =
            $@"
            <WeatherData>
                <Location>{_fakeWeatherData.Location}</Location>
                <Temperature>{_fakeWeatherData.Temperature}</Temperature>
                <Humidity>{_fakeWeatherData.Humidity}</Humidity>
            </WeatherData>";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));

        string? capturedFilePath = null;
        _mockFileStreamWrapper
            .Setup(x => x.GetAsyncStream(It.IsAny<string>()))
            .Callback<string>((filePath) =>
            {
                capturedFilePath = filePath;
            })
            .Returns(fakeStream);

        var fakeSource = _fixture.Create<string>();

        var sut = new WeatherDataRetrieverXml(
            fakeSource,
            _mockFileStreamWrapper.Object);

        //Act
        var result = await sut.GetWeatherData();

        //Assert
        capturedFilePath.Should().Be(fakeSource);
        result.Should().BeEquivalentTo(_fakeWeatherData);
    }

    [Fact]
    public async Task ThrowXmlSchemaExceptionOnInvalidXml()
    {
        //Arrange
        var invalidXmlContent =
            $@"
            <WeatherData>
                <Location>{_fakeWeatherData.Location}</Location>
                <Outlook>{_fakeWeatherData.Temperature}</Outlook>
                <Humidity>{_fakeWeatherData.Humidity}</Humidity>
            </WeatherData>";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidXmlContent));

        _mockFileStreamWrapper
            .Setup(x => x.GetAsyncStream(It.IsAny<string>()))
            .Returns(fakeStream);

        var fakeSource = _fixture.Create<string>();

        var sut = new WeatherDataRetrieverXml(
            fakeSource,
            _mockFileStreamWrapper.Object);

        //Act and Assert
        await sut
            .Invoking(x => x.GetWeatherData())
            .Should()
            .ThrowAsync<XmlSchemaException>();
    }
}
