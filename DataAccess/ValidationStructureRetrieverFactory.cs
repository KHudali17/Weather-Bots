namespace WeatherBots.DataAccess
{
    public class ValidationStructureRetrieverFactory
    {
        private readonly Dictionary<SupportedSourcesEnum, string> 
            _typeToValidationFilePathDict = new Dictionary<SupportedSourcesEnum, string>();

        public ValidationStructureRetrieverFactory()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string pathToJsonValidation = Path.Combine(currentDirectory, "JSON_validation.json");

            string pathToXmlValidation = Path.Combine(currentDirectory, "XML_validation.xml");

            _typeToValidationFilePathDict.Add(SupportedSourcesEnum.JSON,  pathToJsonValidation);
            _typeToValidationFilePathDict.Add(SupportedSourcesEnum.XML, pathToXmlValidation);
            
        }

        public IValidationStructureRetriever GetValidationRetriever(SupportedSourcesEnum? sourceType)
        {
            switch (sourceType)
            {
                case SupportedSourcesEnum.JSON:
                    return new ValidationStructureRetrieverJson(_typeToValidationFilePathDict[SupportedSourcesEnum.JSON]);

                case SupportedSourcesEnum.XML:
                    return new ValidationStructureRetrieverXml(_typeToValidationFilePathDict[SupportedSourcesEnum.XML]);

                default:
                    /*
                     * Case when a new source is added to enum but factory
                     * is not yet updated (should be unreachable).
                     */
                    throw new NotImplementedException();
            }
        }
    }
}