using System;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractTamagochiTimer
    {
        public abstract int Year { get; protected set; }

        public abstract int Month { get; protected set; }

        public abstract int Day { get; protected set; }

        public abstract int Hour { get; protected set; }

        public abstract TimeSpan CurrentTime { get; }

        public EventHandler<HourChangedEventArgs> HourChanged;

        public EventHandler<DayChangedEventArgs> DayChanged;

        public EventHandler<YearChangedEventArgs> YearChanged;

        public EventHandler<MonthChangedEventArgs> MonthChanged;

        public abstract void InitializeTimer(TimeSpan time);

        public abstract void StartTimer();

        public abstract void StopTimer();
    }
}