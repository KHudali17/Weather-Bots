using WeatherBots.Seams;

namespace WeatherBots.DataAccess;

public class WeatherDataRetrieverFactory<T>
{
    public IWeatherDataRetriever GetWeatherDataRetriever(T source)
    {
        SupportedSourcesEnum? sourceType = null;

        try
        {
            sourceType = DataRetrievalUtility.GetSourceType(source);
        }
        catch (Exception)
        {
            throw;
        }

        return sourceType switch
        {
            SupportedSourcesEnum.JSON => new WeatherDataRetrieverJson((source as string)!, new FileStreamWrapper()),
            SupportedSourcesEnum.XML => new WeatherDataRetrieverXml((source as string)!, new FileStreamWrapper()),
            _ => throw new NotImplementedException(),
            /*
             * Case when a new source is added to enum but factory
             * is not yet updated; otherwise unreachable.
             */
        };
    }
}
