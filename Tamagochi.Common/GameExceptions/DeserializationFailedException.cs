using Tamagochi.Common.Properties;

namespace Tamagochi.Common.GameExceptions
{
    public class DeserializationFailedException : TamagochiBaseException
    {
        public string DeserializedTypeName { get; }

        public DeserializationFailedException(ExceptionDetails exceptionDetails) : this(exceptionDetails, string.Empty)
        {
        }

        public DeserializationFailedException(ExceptionDetails exceptionDetails, string typeName) : this(exceptionDetails, typeName, Resources.SerializationFailedDefaultMessage)
        {
        }

        public DeserializationFailedException(ExceptionDetails exceptionDetails, string typeName, string message) : base(exceptionDetails, message)
        {
            DeserializedTypeName = typeName;
        }
    }
}