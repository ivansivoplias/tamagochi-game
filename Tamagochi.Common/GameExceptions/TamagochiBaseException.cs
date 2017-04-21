using System;

namespace Tamagochi.Common.GameExceptions
{
    public class TamagochiBaseException : Exception
    {
        private ExceptionDetails _details;

        public string ClassName => _details.ClassName;
        public string CallerMethodName => _details.CallerMethodName;
        public string OriginalExceptionMessage => _details.OriginalExceptionMessage;
        public string OriginalExceptionStackTrace => _details.StackTrace;

        public TamagochiBaseException(ExceptionDetails exceptionDetails) : base(GameExceptionMessages.BaseExceptionDefaultMessage)
        {
            _details = exceptionDetails;
        }

        public TamagochiBaseException(ExceptionDetails exceptionDetails, string message) : base(message)
        {
            _details = exceptionDetails;
        }
    }
}