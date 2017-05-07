using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class HourChangedEventArgs : EventArgs
    {
        private TimeSpan _currentTime;
        private int _currentHour;

        public int GameHour => _currentHour;
        public TimeSpan CurrentTime => _currentTime;

        public HourChangedEventArgs(int hour, TimeSpan time)
        {
            _currentHour = hour;
            _currentTime = time;
        }
    }
}