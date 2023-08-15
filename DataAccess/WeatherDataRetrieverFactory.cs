using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherBots.DataAccess
{
    public class WeatherDataRetrieverFactory<T>
    {
        public IWeatherDataRetriever GetWeatherDataRetriever(T source, ValidationStructureRetrieverFactory validationFactory)
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

            IValidationStructureRetriever validation = validationFactory.GetValidationRetriever(sourceType);

            switch (sourceType)
            {
                case SupportedSourcesEnum.JSON:
                    return new WeatherDataRetrieverJson( (source as string)!, validation);

                case SupportedSourcesEnum.XML:
                    return new WeatherDataRetrieverXml( (source as string)!, validation);

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
