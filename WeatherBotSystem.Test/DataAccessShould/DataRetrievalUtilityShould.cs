using AutoFixture;
using FluentAssertions;
using WeatherBots.DataAccess;

namespace WeatherBotSystem.Test.DataAccessShould;

public class DataRetrievalUtilityShould
{
    private readonly Fixture _fixture;

    public DataRetrievalUtilityShould()
    {
        _fixture = new Fixture();
    }

    [Fact]
    public void ThrowUnsupportedDataFromatExceptionWhenSourceIsNotString()
    {
        //Arrange
        var action = () => DataRetrievalUtility.GetSourceType(_fixture.Create<int>());

        //Assert
        action.Should().Throw<UnsupportedDataFormatException>();
    }

    [Fact]
    public void ThrowUnsupportedDataFromatExceptionWhenSourceIsStringButInvalid()
    {
        //Arrange
        var action = () => DataRetrievalUtility.GetSourceType(_fixture.Create<string>());

        //Assert
        action.Should().Throw<UnsupportedDataFormatException>();
    }

    [Fact]
    public void ReturnSupportedSourcesXmlWhenValidString()
    {
        //Arrange
        var validStringForXml = _fixture.Create<string>() + ".xml";

        //Act
        var result = DataRetrievalUtility.GetSourceType(validStringForXml);

        //Assert
        result.Should().Be(SupportedSourcesEnum.XML);
    }

    [Fact]
    public void ReturnSupportedSourcesJsonWhenValidString()
    {
        //Arrange
        var validStringForJson = _fixture.Create<string>() + ".json";

        //Act
        var result = DataRetrievalUtility.GetSourceType(validStringForJson);

        //Assert
        result.Should().Be(SupportedSourcesEnum.JSON);
    }
}
