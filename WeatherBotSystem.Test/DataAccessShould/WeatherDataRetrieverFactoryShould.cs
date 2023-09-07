using AutoFixture;
using FluentAssertions;
using System.Reflection;
using WeatherBots.DataAccess;

namespace WeatherBotSystem.Test.DataAccessShould;

public class WeatherDataRetrieverFactoryShould
{
    private readonly Fixture _fixture;

    public WeatherDataRetrieverFactoryShould()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ThrowUnsupportedDataFromatExceptionWhenSourceTypeUnsupported()
    {
        //Arrange
        var sut = new WeatherDataRetrieverFactory<int>();
        var action = () => sut.GetWeatherDataRetriever(_fixture.Create<int>());

        //Assert
        action.Should().Throw<UnsupportedDataFormatException>();
    }

    [Fact]
    public void ReturnJsonRetrieverWhenSourceIsJson()
    {
        //Arrange
        string jsonSource = _fixture.Create<string>() + ".json";
        var sut = new WeatherDataRetrieverFactory<string>();

        //Act
        var result = sut.GetWeatherDataRetriever(jsonSource);

        //Assert
        result.Should().BeOfType(typeof(WeatherDataRetrieverJsonShould));

        var sourceInternalField = GetSourceField(result);
        sourceInternalField.Should().Be(jsonSource);
    }

    [Fact]
    public void ReturnXmlRetrieverWhenSourceIsXml()
    {
        //Arrange
        string xmlSource = _fixture.Create<string>() + ".xml";
        var sut = new WeatherDataRetrieverFactory<string>();

        //Act
        var result = sut.GetWeatherDataRetriever(xmlSource);

        //Assert
        result.Should().BeOfType(typeof(WeatherDataRetrieverXml));

        var sourceInternalField = GetSourceField(result);
        sourceInternalField.Should().Be(xmlSource);
    }

    private string GetSourceField(IWeatherDataRetriever retriever)
    {
        var fieldInfo = retriever
            .GetType()
            .GetField("_source", BindingFlags.NonPublic | BindingFlags.Instance);

        return (string)fieldInfo!.GetValue(retriever)!;
    }
}
