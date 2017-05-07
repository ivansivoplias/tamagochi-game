using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class MinuteChangedEventArgs : EventArgs
    {
        public TimeSpan NewTime { get; }

        public MinuteChangedEventArgs(TimeSpan newTime)
        {
            NewTime = newTime;
        }
    }
}