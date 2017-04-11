using System;
using System.Timers;
using Tamagochi.Abstract;

namespace Tamagochi.Models
{
    public class TamagochiTimer : AbstractTamagochiTimer
    {
        private Timer _timer;
        private TimeSpan _currentTime;
        private DateTime _date;
        private static readonly Lazy<TamagochiTimer> _instance = new Lazy<TamagochiTimer>(() => new TamagochiTimer(), true);

        public override int Years => _date.Year;

        public override int Months => _date.Month;

        public override int Days => _date.Day;

        public override int Hours => _date.Hour;

        private TamagochiTimer()
        {
            _timer = new Timer();
            _timer.AutoReset = false;
            _timer.Elapsed += RealMinuteChanged;
            _timer.Interval = GetInterval();
        }

        public override void StartTimer()
        {
            _timer.Start();
        }

        public static AbstractTamagochiTimer GetInstance()
        {
            return _instance.Value;
        }

        public override void SaveTimeOnGameClosing(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void InitializeTimer(TimeSpan time)
        {
            _currentTime = time;
            _date = DateFromTimeSpan(_currentTime);
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
            _currentTime.Add(TimeSpan.FromHours(1));
            _date = DateFromTimeSpan(_currentTime);
            _timer.Interval = GetInterval();
            StartTimer();
        }
    }
}