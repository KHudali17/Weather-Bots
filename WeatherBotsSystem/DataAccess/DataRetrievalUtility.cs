namespace WeatherBots.DataAccess;

public class DataRetrievalUtility
{
    public static SupportedSourcesEnum GetSourceType<T>(T source)
    {
        if (source is string)
        {
            return GetSupportedTypeFromString((source as string)!);
        }

        throw new UnsupportedDataFormatException();
    }

    private static SupportedSourcesEnum GetSupportedTypeFromString(string source)
    {

        if (source.EndsWith(".xml")) return SupportedSourcesEnum.XML;

        if (source.EndsWith(".json")) return SupportedSourcesEnum.JSON;

        throw new UnsupportedDataFormatException();
    }
}