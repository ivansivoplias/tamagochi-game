namespace Tamagochi.Common
{
    public struct ExceptionDetails
    {
        public string ClassName { get; set; }
        public string CallerMethodName { get; set; }
        public string OriginalExceptionMessage { get; set; }
        public string StackTrace { get; set; }
    }
}