using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using WeatherBots.Prompts;
using WeatherBots.Seams;

namespace WeatherBotSystem.Test.PromptShould;

public class PrompterShould
{
    private readonly Mock<IConsoleWrapper> _mockConsoleWrapper;
    private readonly Mock<IFileWrapper> _mockFileWrapper;
    private readonly Fixture _fixture;

    public PrompterShould()
    {
        _mockConsoleWrapper = new Mock<IConsoleWrapper>();
        _mockFileWrapper = new Mock<IFileWrapper>();
        _fixture = new Fixture();
    }

    [Theory]
    [AutoData]
    public async Task PrintMessageToConsole(string message)
    {
        //Arrange
        string? capturedMessage = null;
        _mockConsoleWrapper
            .Setup(x => x.WriteLineAsync(It.IsAny<string>()))
            .Callback<string>((message) =>
            {
                capturedMessage = message;
            });

        _mockFileWrapper
            .Setup(x => x.Exists(It.IsAny<string>()))
            .Returns(true);

        var sut = new Prompter(
            _mockConsoleWrapper.Object,
            _mockFileWrapper.Object);

        //Act
        await sut.PromptFilePathAsync(message);

        //Assert
        capturedMessage.Should().Be(message);
    }

    [Fact]
    public async Task WaitForUserInputAfterPrintingMessage()
    {
        //Arrange
        _mockFileWrapper
            .Setup(x => x.Exists(It.IsAny<string>()))
            .Returns(true);

        var sut = new Prompter(
            _mockConsoleWrapper.Object,
            _mockFileWrapper.Object);

        //Act
        await sut.PromptFilePathAsync(_fixture.Create<string>());

        //Assert
        _mockConsoleWrapper.Verify(x => x.ReadLineAsync(), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task ReturnUserInputWhenValid(string? ValiduserInput)
    {
        //Arrange
        _mockConsoleWrapper
            .Setup(x => x.ReadLineAsync())
            .Returns(Task.FromResult(ValiduserInput));

        _mockFileWrapper
            .Setup(x => x.Exists(It.IsAny<string?>()))
            .Returns(true);

        var sut = new Prompter(
            _mockConsoleWrapper.Object,
            _mockFileWrapper.Object);

        //Act
        var result = await sut.PromptFilePathAsync(_fixture.Create<string>());

        //Assert
        result.Should().Be(ValiduserInput);
    }

    [Fact]
    public async Task ThrowFileNotFoundExceptionWhenInvalidUserInput()
    {
        //Arrange
        _mockFileWrapper
            .Setup(x => x.Exists(It.IsAny<string?>()))
            .Returns(false);

        var sut = new Prompter(
            _mockConsoleWrapper.Object,
            _mockFileWrapper.Object);

        //Act and Assert
        await sut.Invoking(x => x.PromptFilePathAsync(It.IsAny<string>()))
           .Should().ThrowAsync<FileNotFoundException>();
    }
}
