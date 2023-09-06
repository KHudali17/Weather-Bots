using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherBots.DataRecords
{
    public record BotSettings
    {
        public required bool Enabled { get; init; }
        public required string Message { get; init; }
        public float? TemperatureThreshold { get; init; } = null;
        public float? HumidityThreshold { get; init; } = null;

        [JsonIgnore]
        public bool HasExactlyOneThreshold
        {
            get
            {
                bool hasOnlyTemperature = HumidityThreshold == null && TemperatureThreshold != null;
                bool hasOnlyHumidity = TemperatureThreshold == null && HumidityThreshold != null;
                return hasOnlyTemperature || hasOnlyHumidity;
            }
        }
    }
}
