using Tamagochi.Common.Properties;

namespace Tamagochi.Common.GameExceptions
{
    public class SerializationFailedException : TamagochiBaseException
    {
        public string SerializedTypeName { get; }

        public SerializationFailedException(ExceptionDetails exceptionDetails) : this(exceptionDetails, string.Empty)
        {
        }

        public SerializationFailedException(ExceptionDetails exceptionDetails, string typeName) : this(exceptionDetails, typeName, Resources.SerializationFailedDefaultMessage)
        {
        }

        public SerializationFailedException(ExceptionDetails exceptionDetails, string typeName, string message) : base(exceptionDetails, message)
        {
            SerializedTypeName = typeName;
        }
    }
}