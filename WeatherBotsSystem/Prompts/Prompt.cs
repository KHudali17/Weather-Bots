using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots
{
    public class Prompt
    {
        public static async Task<string> PromptFilePathAsync(string message)
        {
            await Console.Out.WriteLineAsync(message);
            string? userInput = await Console.In.ReadLineAsync();
            ValidateInputPath(userInput);
            return userInput!;
        }

        private static bool ValidateInputPath(string? inputPath) => File.Exists(inputPath) ? true : throw new FileNotFoundException();
    }
}
