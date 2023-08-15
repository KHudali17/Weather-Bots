namespace WeatherBots.DataAccess
{
    internal class DataRetrievalUtility
    {
        internal static SupportedSourcesEnum getSourceType<T>(T source)
        {
            if ( source is string )
            {
                return getSupportedTypeFromString( (source as string)! );
            }

            throw new UnsupportedDataFormatException();
        }

        private static SupportedSourcesEnum getSupportedTypeFromString(string source)
        {
            
            if ( source.EndsWith(".xml") ) return SupportedSourcesEnum.XML;

            if ( source.EndsWith(".json") ) return SupportedSourcesEnum.JSON;

            throw new UnsupportedDataFormatException();
        }
    }
}