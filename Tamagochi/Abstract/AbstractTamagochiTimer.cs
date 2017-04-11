using System;
using System.ComponentModel;

namespace Tamagochi.Abstract
{
    public abstract class AbstractTamagochiTimer
    {
        public abstract int Years { get; }

        public abstract int Months { get; }

        public abstract int Days { get; }

        public abstract int Hours { get; }

        public EventHandler<EventArgs> HourChanged;

        public abstract void InitializeTimer(TimeSpan time);

        public abstract void StartTimer();

        public abstract void SaveTimeOnGameClosing(object sender, EventArgs e);
    }
}