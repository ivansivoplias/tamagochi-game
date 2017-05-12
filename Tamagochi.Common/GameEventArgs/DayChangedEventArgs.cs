using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class DayChangedEventArgs : EventArgs
    {
        private int _currentDay;

        public int CurrentGameDay => _currentDay;

        public DayChangedEventArgs(int currentDay)
        {
            _currentDay = currentDay;
        }
    }
}