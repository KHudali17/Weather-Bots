namespace WeatherBots.DataAccess
{
    internal class ValidationStructureRetrieverXml : IValidationStructureRetriever
    {
        private string _path;

        public ValidationStructureRetrieverXml(string path)
        {
            _path = path;
        }
    }
}