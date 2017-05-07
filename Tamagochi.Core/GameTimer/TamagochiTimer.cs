using System;
using System.Timers;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Common;

namespace Tamagochi.Core.GameTimer
{
    public class TamagochiTimer : AbstractTamagochiTimer
    {
        private Timer _timer;
        private int _hour;
        private int _minute;
        private TimeSpan _realTime;
        private TimerState _timerState;
        private static readonly Lazy<TamagochiTimer> _instance = new Lazy<TamagochiTimer>(() => new TamagochiTimer(), true);

        public override event EventHandler<HourChangedEventArgs> RealHourChanged;

        public override event EventHandler<MinuteChangedEventArgs> RealMinuteChanged;

        public override int RealHour
        {
            get { return _hour; }
            protected set
            {
                _hour = value;
                RealHourChanged?.Invoke(this, new HourChangedEventArgs(_hour, _realTime));
            }
        }

        public override int RealMinute
        {
            get
            {
                return _minute;
            }

            protected set
            {
                _minute = value;
                RealMinuteChanged?.Invoke(this, new MinuteChangedEventArgs(_realTime));
            }
        }

        public override TimeSpan RealTime => _realTime;

        public override TimerState State
        {
            get { return _timerState; }
            protected set
            {
                _timerState = value;
            }
        }

        private TamagochiTimer()
        {
            State = TimerState.Inactive;
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Elapsed += RealMinuteChangedHandler;
            _timer.Interval = GetInterval();
        }

        public override void StartTimer()
        {
            _timer.Start();
            State = TimerState.Active;
        }

        public static AbstractTamagochiTimer GetInstance()
        {
            return _instance.Value;
        }

        private static double GetInterval()
        {
            DateTime now = DateTime.Now;
            return ((now.Second > 30 ? 120 : 60) - now.Second) * 1000 - now.Millisecond;
        }

        private static DateTime DateFromTimeSpan(TimeSpan time)
        {
            return new DateTime(time.Ticks);
        }

        private void RealMinuteChangedHandler(object sender, ElapsedEventArgs e)
        {
            _realTime.Add(TimeSpan.FromMinutes(1));

            RealMinute = _realTime.Minutes;
            if (_realTime.Hours > RealHour)
            {
                RealHour = _realTime.Hours;
            }

            _timer.Interval = GetInterval();

            StartTimer();
        }

        public override void StopTimer()
        {
            _timer.Stop();
            _timerState = TimerState.Stopped;
        }
    }
}