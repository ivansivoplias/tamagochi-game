using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class HourChangedEventArgs : EventArgs
    {
        private TimeSpan _currentGameTime;
        private int _currentHour;

        public int GameHour => _currentHour;
        public TimeSpan CurrentGameTime => _currentGameTime;

        public HourChangedEventArgs(int hour, TimeSpan time)
        {
            _currentHour = hour;
            _currentGameTime = time;
        }
    }
}