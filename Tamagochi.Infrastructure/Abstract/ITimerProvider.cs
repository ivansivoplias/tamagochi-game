namespace Tamagochi.Infrastructure.Abstract
{
    public interface ITimerProvider
    {
        AbstractTamagochiTimer GetTimer();
    }
}