using System;

namespace Tamagochi.Common.GameExceptions
{
    public class VerificationException : Exception
    {
        public VerificationException(string message) : base(message)
        {
        }
    }
}