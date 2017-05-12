using System;

namespace Tamagochi.Common.GameExceptions
{
    public class SelectImageException : Exception
    {
        public SelectImageException(string message) : base(message)
        {
        }
    }
}