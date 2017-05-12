using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class GameTimeChangedEventArgs : EventArgs
    {
        public TimeSpan CurrentGameTime { get; }

        public GameTimeChangedEventArgs(TimeSpan time)
        {
            CurrentGameTime = time;
        }
    }
}