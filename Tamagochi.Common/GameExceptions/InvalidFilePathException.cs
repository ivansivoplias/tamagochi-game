using Tamagochi.Common.Properties;

namespace Tamagochi.Common.GameExceptions
{
    public class InvalidFilePathException : TamagochiBaseException
    {
        public string SerializationFilePath { get; }

        public InvalidFilePathException(ExceptionDetails details) : this(details, string.Empty)
        {
        }

        public InvalidFilePathException(ExceptionDetails details, string filePath) : this(details, filePath, Resources.InvalidFileMessage)
        {
        }

        public InvalidFilePathException(ExceptionDetails details, string filePath, string message) : base(details, message)
        {
            SerializationFilePath = filePath;
        }
    }
}