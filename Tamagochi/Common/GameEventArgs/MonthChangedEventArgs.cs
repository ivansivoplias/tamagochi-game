using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Common.GameEventArgs
{
    public class MonthChangedEventArgs : EventArgs
    {
        private int _currentGameMonth;

        public int CurrentGameMonth => _currentGameMonth;

        public MonthChangedEventArgs(int month)
        {
            _currentGameMonth = month;
        }
    }
}