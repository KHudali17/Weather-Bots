using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataRecords
{
    public record BotConfig : IConfigData
    {
        public required BotSettings SunBot { get; init; }
        public required BotSettings RainBot { get; init; }
        public required BotSettings SnowBot { get; init; }
    }
}
