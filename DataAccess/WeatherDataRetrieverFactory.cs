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

            switch (sourceType)
            {
                case SupportedSourcesEnum.JSON:
                    return new WeatherDataRetrieverJson( (source as string)! );

                case SupportedSourcesEnum.XML:
                    return new WeatherDataRetrieverXml( (source as string)! );

                default:
                    /*
                     * Case when a new source is added to enum but factory
                     * is not yet updated; otherwise unreachable.
                     */
                    throw new NotImplementedException();
            }
        }
    }
}
