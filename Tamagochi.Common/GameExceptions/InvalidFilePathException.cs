namespace Tamagochi.Common.GameExceptions
{
    public class InvalidFilePathException : TamagochiBaseException
    {
        public string SerializationFilePath { get; }
        private const string InvalidFileMessage = "Path of the file is invalid.";

        public InvalidFilePathException(string className, string filePath) : this(className, filePath, InvalidFileMessage)
        {
        }

        public InvalidFilePathException(string className, string filePath, string message) : base(className, message)
        {
            SerializationFilePath = filePath;
        }
    }
}