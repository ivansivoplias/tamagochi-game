using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class SecondChangedEventArgs : EventArgs
    {
        public int CurrentSecond { get; }
        public TimeSpan CurrentTime { get; }

        public SecondChangedEventArgs(TimeSpan newValue)
        {
            CurrentTime = newValue;
            CurrentSecond = newValue.Seconds;
        }
    }
}