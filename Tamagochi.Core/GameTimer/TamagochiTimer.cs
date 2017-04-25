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
        private TimeSpan _currentTime;
        private int _year;
        private int _day;
        private int _month;
        private int _hour;
        private TimerState _timerState;
        private static readonly Lazy<TamagochiTimer> _instance = new Lazy<TamagochiTimer>(() => new TamagochiTimer(), true);

        public override event EventHandler<HourChangedEventArgs> HourChanged;

        public override event EventHandler<DayChangedEventArgs> DayChanged;

        public override event EventHandler<YearChangedEventArgs> YearChanged;

        public override event EventHandler<MonthChangedEventArgs> MonthChanged;

        public override int Year
        {
            get { return _year; }
            protected set
            {
                _year = value;
                YearChanged?.Invoke(this, new YearChangedEventArgs(_year));
            }
        }

        public override int Month
        {
            get { return _month; }
            protected set
            {
                _month = value;
                MonthChanged?.Invoke(this, new MonthChangedEventArgs(_month));
            }
        }

        public override int Day
        {
            get { return _day; }
            protected set
            {
                _day = value;
                DayChanged?.Invoke(this, new DayChangedEventArgs(_day));
            }
        }

        public override int Hour
        {
            get { return _hour; }
            protected set
            {
                _hour = value;
                HourChanged?.Invoke(this, new HourChangedEventArgs(_hour, _currentTime));
            }
        }

        public override TimeSpan CurrentTime => _currentTime;

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
            _timer.Elapsed += RealMinuteChanged;
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

        public override void InitializeTimer(TimeSpan time)
        {
            _currentTime = time;
            DateTime date = DateFromTimeSpan(_currentTime);
            _day = date.Day;
            _hour = date.Hour;
            _month = date.Month;
            _year = date.Year;
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

        private void RealMinuteChanged(object sender, ElapsedEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromHours(1));
            DateTime date = DateFromTimeSpan(_currentTime);
            _timer.Interval = GetInterval();

            Hour++;
            if (date.Day > Day)
            {
                Day = date.Day;
            }

            if (date.Month > Month)
            {
                Month = date.Month;
            }

            if (date.Year > Year)
            {
                Year = date.Year;
            }

            StartTimer();
        }

        public override void StopTimer()
        {
            _timer.Stop();
            _timerState = TimerState.Stopped;
        }
    }
}