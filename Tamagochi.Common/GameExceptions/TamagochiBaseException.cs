using System;
using Tamagochi.Common.Properties;

namespace Tamagochi.Common.GameExceptions
{
    public class TamagochiBaseException : Exception
    {
        private ExceptionDetails _details;

        public string ClassName => _details.ClassName;
        public string CallerMethodName => _details.CallerMethodName;
        public string OriginalExceptionMessage => _details.OriginalExceptionMessage;
        public string OriginalExceptionStackTrace => _details.StackTrace;

        public TamagochiBaseException(ExceptionDetails exceptionDetails) : base(Resources.BaseExceptionMessage)
        {
            _details = exceptionDetails;
        }

        public TamagochiBaseException(ExceptionDetails exceptionDetails, string message) : base(message)
        {
            _details = exceptionDetails;
        }
    }
}