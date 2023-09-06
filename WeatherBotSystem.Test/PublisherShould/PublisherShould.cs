using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using System.Reflection;
using WeatherBots.ObserversAndPublishers;

namespace WeatherBotSystem.Test.PublisherShould;

public class PublisherShould
{
    private readonly Mock<IPublisherObserver<int>> _mockObserver;
    private readonly Publisher<int> _sut;

    public PublisherShould()
    {
        _mockObserver = new Mock<IPublisherObserver<int>>();
        _sut = new Publisher<int>();
    }

    [Fact]
    public void AddObservers()
    {
        //Act
        var result = _sut.AddObserver(_mockObserver.Object);

        //Assert
        result.Should().BeTrue();

        var observersPrivateField = getObserversField(_sut);
        observersPrivateField
            .First().Should().Be(_mockObserver.Object);
    }

    [Fact]
    public void RemoveObservers()
    {
        //Arrange
        _sut.AddObserver(_mockObserver.Object);

        //Act
        var result = _sut.RemoveObserver(_mockObserver.Object);

        //Assert
        result.Should().BeTrue();

        var observersPrivateField = getObserversField(_sut);
        observersPrivateField.Should().BeEmpty();
    }

    [Theory]
    [AutoData]
    public void NotifyObservers(int data)
    {
        //Arrange
        int? capturedData = null;
        _mockObserver
            .Setup(x => x.Update(It.IsAny<int>()))
            .Callback<int>((updateData) =>
            {
                capturedData = updateData;
            });

        _sut.AddObserver(_mockObserver.Object);

        //Act
        var result = _sut.NotifyObservers(data);

        //Assert
        result.Should().BeTrue();
        capturedData.Should().Be(data);
    }

    private List<IPublisherObserver<int>> getObserversField(Publisher<int> publisher)
    {
        var fieldInfo = publisher
            .GetType()
            .GetField("_observers", BindingFlags.NonPublic | BindingFlags.Instance);

        return (List<IPublisherObserver<int>>)fieldInfo!.GetValue(_sut)!;
    }
}
