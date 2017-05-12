namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractGameFactory
    {
        public abstract AbstractGame MakeGame(IGameContext context);
    }
}