using System.Runtime.Serialization;

namespace WeatherBots.DataAccess
{
    [Serializable]
    internal class UnsupportedDataFormatException : Exception
    {
        public UnsupportedDataFormatException()
        {
        }

        public UnsupportedDataFormatException(string? message) : base(message)
        {
        }

        public UnsupportedDataFormatException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnsupportedDataFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}