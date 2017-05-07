using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Common
{
    public static class GameTimeHelper
    {
        public static GameDate GetGameDate(this TimeSpan time)
        {
            var totalDays = time.TotalDays;
            var days = (int)Math.Truncate(totalDays % 30);
            var years = (int)Math.Truncate(totalDays / 365);
            var month = (int)Math.Truncate((totalDays - years * 365) / 30);
            return new GameDate(days, month, years);
        }
    }
}