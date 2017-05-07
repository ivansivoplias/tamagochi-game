using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class GameTimeChangedEventArgs : EventArgs
    {
        public int CurrentGameHours { get; }
        public int CurrentGameMinutes { get; }
        public int CurrentGameMonths { get; }
        public int CurrentGameSeconds { get; }
        public int CurrentGameYears { get; }

        public TimeSpan CurrentGameTime { get; }

        public GameTimeChangedEventArgs(TimeSpan time)
        {
            CurrentGameTime = time;
            CurrentGameHours = time.Hours;
            CurrentGameMinutes = time.Minutes;
            CurrentGameMonths = new DateTime(time.Ticks).Month;
            CurrentGameSeconds = time.Seconds;
            CurrentGameYears = new DateTime(time.Ticks).Year;
        }
    }
}