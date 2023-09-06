using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverFactory<T>
    {
        public IWeatherDataRetriever GetWeatherDataRetriever(T source)
        {
            SupportedSourcesEnum? sourceType = null;
            
            try
            {
                sourceType = DataRetrievalUtility.getSourceType(source);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return sourceType switch
            {
                SupportedSourcesEnum.JSON => new WeatherDataRetrieverJson((source as string)!),
                SupportedSourcesEnum.XML => new WeatherDataRetrieverXml((source as string)!),
                _ => throw new NotImplementedException(),
                    /*
                     * Case when a new source is added to enum but factory
                     * is not yet updated; otherwise unreachable.
                     */
            };
        }
    }
}
