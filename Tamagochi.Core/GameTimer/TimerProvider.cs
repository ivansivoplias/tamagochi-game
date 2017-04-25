using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.GameTimer
{
    public class TimerProvider : ITimerProvider
    {
        public AbstractTamagochiTimer GetTimer()
        {
            return TamagochiTimer.GetInstance();
        }
    }
}