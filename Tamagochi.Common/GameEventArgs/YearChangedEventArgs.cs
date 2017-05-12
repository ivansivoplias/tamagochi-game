using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class YearChangedEventArgs : EventArgs
    {
        private int _currentYear;

        public int CurrentGameYear => _currentYear;

        public YearChangedEventArgs(int currentYear)
        {
            _currentYear = currentYear;
        }
    }
}