using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractTamagochiTimer
    {
        public abstract TimerState State { get; protected set; }

        public abstract int RealHour { get; protected set; }

        public abstract int RealMinute { get; protected set; }

        public abstract int RealSecond { get; protected set; }

        public abstract TimeSpan RealTime { get; }

        public abstract event EventHandler<HourChangedEventArgs> RealHourChanged;

        public abstract event EventHandler<MinuteChangedEventArgs> RealMinuteChanged;

        public abstract event EventHandler<SecondChangedEventArgs> RealSecondChanged;

        public abstract void StartTimer();

        public abstract void StopTimer();
    }
}