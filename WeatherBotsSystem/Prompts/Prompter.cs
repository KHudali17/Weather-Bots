using WeatherBots.Seams;

namespace WeatherBots.Prompts;

public class Prompter
{
    private readonly IConsoleWrapper _consoleWrapper;
    private readonly IFileWrapper _fileWrapper;

    public Prompter(IConsoleWrapper consoleWrapper, IFileWrapper fileWrapper)
    {
        _consoleWrapper = consoleWrapper;
        _fileWrapper = fileWrapper;
    }

    public async Task<string> PromptFilePathAsync(string message)
    {
        await _consoleWrapper.WriteLineAsync(message);
        string? userInput = await _consoleWrapper.ReadLineAsync();
        ValidateInputPath(userInput);
        return userInput!;
    }

    private bool ValidateInputPath(string? inputPath) 
        => _fileWrapper.Exists(inputPath) ? true : throw new FileNotFoundException();
}
