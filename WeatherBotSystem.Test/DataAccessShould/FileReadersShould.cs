using AutoFixture;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using WeatherBots.DataAccess;
using WeatherBots.DataRecords;
using WeatherBotsSystem.Test.TestHelpers;

namespace WeatherBotSystem.Test.DataAccessShould;

public class FileReadersShould
{
    private readonly Fixture _fixture;
    private readonly WeatherData _fakeWeatherData;

    public FileReadersShould()
    {
        _fixture = new Fixture();
        _fixture.Customizations.Add(new ValidWeatherDataAutoFixtureBuilder());
        _fakeWeatherData = _fixture.Create<WeatherData>();
    }

    [Fact]
    public async Task DeserializeValidJsonMatchingPropertyCase()
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

        //Act
        var result = await JsonFileReader.ReadJsonFileAsync<WeatherData>(fakeStream);

        //Assert
        result.Should().BeEquivalentTo(_fakeWeatherData);
    }

    [Fact]
    public async Task DeserializeValidJsonPropertyCaseInsensitive()
    {
        //Arrange
        var jsonContent =
            $@"
            {{
                ""locaTion"": ""{_fakeWeatherData.Location}"",
                ""temperaturE"": {_fakeWeatherData.Temperature},
                ""humiditY"": {_fakeWeatherData.Humidity}
            }}";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

        //Act
        var result = await JsonFileReader.ReadJsonFileAsync<WeatherData>(fakeStream);

        //Assert
        result.Should().BeEquivalentTo(_fakeWeatherData);
    }

    [Fact]
    public async Task ThrowJsonExceptionOnInvalidJson()
    {
        //Arrange
        var invalidJsonContent =
            $@"
            {{
                ""noitacoL"": ""{_fakeWeatherData.Location}"",
                ""erutarepmeT"": {_fakeWeatherData.Temperature},
                ""ytidimuH"": {_fakeWeatherData.Humidity}
            }}";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(invalidJsonContent));

        //Act and assert
        var action = async () => await JsonFileReader.ReadJsonFileAsync<WeatherData>(fakeStream);

        //Assert
        await action.Should().ThrowAsync<JsonException>();
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

        //Act
        var result = await XmlFileReader.ReadXmlFileAsync<WeatherData>(fakeStream);

        //Assert
        result.Should().BeEquivalentTo(_fakeWeatherData);
    }

    [Fact]
    public async Task ReturnObjectWithNullsOnInvalidXml()
    {
        //Arrange
        var xmlContent =
            $@"
            <WeatherData>
                <noitacoL>{_fakeWeatherData.Location}</noitacoL>
                <erutarepmeT>{_fakeWeatherData.Temperature}</erutarepmeT>
                <ytidimuH>{_fakeWeatherData.Humidity}</ytidimuH>
            </WeatherData>";

        Stream fakeStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlContent));

        var expected = new WeatherData() { 
            Humidity = null,
            Location = null, 
            Temperature = null 
        };
        //Act
        var result = await XmlFileReader.ReadXmlFileAsync<WeatherData>(fakeStream);

        //Assert
        result.Should().BeEquivalentTo(expected);
    }
}
