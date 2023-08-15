namespace WeatherBots.DataAccess
{
    public class ValidationStructureRetrieverJson : IValidationStructureRetriever
    {
        private readonly string _path;

        public ValidationStructureRetrieverJson(string path)
        {
            _path = path;
        }
    }
}