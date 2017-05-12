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

        public float GetFloatYear()
        {
            float year = 0;
            if (Year > 0)
            {
                year = Year + ((float)Month / 12);
            }
            return year;
        }

        public static bool operator >(GameDate one, GameDate two)
        {
            return one.CompareTo(two) == 1;
        }

        public static bool operator <(GameDate one, GameDate two)
        {
            return one.CompareTo(two) == -1;
        }

        public static bool operator ==(GameDate one, GameDate two)
        {
            return one.CompareTo(two) == 0;
        }

        public static bool operator !=(GameDate one, GameDate two)
        {
            return one.CompareTo(two) != 0;
        }

        public static implicit operator GameDate(TimeSpan time)
        {
            var totalDays = time.TotalDays;
            var days = (int)Math.Truncate(totalDays % 30);
            var years = (int)Math.Truncate(totalDays / 365);
            var month = (int)Math.Truncate((totalDays - years * 365) / 30);
            return new GameDate(days, month, years);
        }
    }
}