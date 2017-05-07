using System;

namespace Tamagochi.Common
{
    public struct GameDate : IComparable<GameDate>, IComparable
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public GameDate(int day, int month, int year)
        {
            Year = year;
            Day = day;
            Month = month;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is GameDate)
            {
                var date = (GameDate)obj;
                return Year == date.Year && Month == date.Month && Day == date.Day;
            }
            return false;
        }

        public int CompareTo(GameDate other)
        {
            int result = -1;
            if (Year > other.Year)
            {
                result = 1;
            }
            else if (Year == other.Year)
            {
                if (Month > other.Month)
                {
                    result = 1;
                }
                else if (Month == other.Month)
                {
                    if (Day > other.Day)
                    {
                        result = 1;
                    }
                    else if (Day == other.Day)
                    {
                        result = 0;
                    }
                }
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            return CompareTo((GameDate)obj);
        }
    }
}