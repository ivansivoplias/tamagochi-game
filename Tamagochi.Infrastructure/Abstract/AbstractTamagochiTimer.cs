using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractTamagochiTimer
    {
        public abstract TimerState State { get; protected set; }

        public abstract int Year { get; protected set; }

        public abstract int Month { get; protected set; }

        public abstract int Day { get; protected set; }

        public abstract int Hour { get; protected set; }

        public abstract TimeSpan CurrentTime { get; }

        public abstract event EventHandler<HourChangedEventArgs> HourChanged;

        public abstract event EventHandler<DayChangedEventArgs> DayChanged;

        public abstract event EventHandler<YearChangedEventArgs> YearChanged;

        public abstract event EventHandler<MonthChangedEventArgs> MonthChanged;

        public abstract void InitializeTimer(TimeSpan time);

        public abstract void StartTimer();

        public abstract void StopTimer();
    }
}