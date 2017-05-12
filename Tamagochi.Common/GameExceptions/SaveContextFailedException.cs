namespace Tamagochi.Common.GameExceptions
{
    public class SaveContextFailedException : TamagochiBaseException
    {
        public SaveContextFailedException(ExceptionDetails exceptionDetails) : base(exceptionDetails)
        {
        }

        public SaveContextFailedException(ExceptionDetails exceptionDetails, string message) : base(exceptionDetails, message)
        {
        }
    }
}