using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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