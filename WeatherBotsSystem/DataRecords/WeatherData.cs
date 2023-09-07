using System.Xml.Serialization;

namespace WeatherBots.DataRecords;

[XmlRoot("WeatherData")]
public record WeatherData
{
    [XmlElement("Location", IsNullable = true)]
    public required string? Location { get; init; }

    [XmlElement("Temperature", IsNullable = true)]
    public required float? Temperature { get; init; }

    [XmlElement("Humidity", IsNullable = true)]
    public required float? Humidity { get; init; }
}
