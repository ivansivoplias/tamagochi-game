namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractGame
    {
        protected IGameContext _context;

        public AbstractGame(IGameContext context)
        {
            _context = context;
        }

        public AbstractTamagochiTimer Timer { get; protected set; }

        public IPet Pet { get; protected set; }

        public abstract void StartGame();

        public abstract void StopGame();

        public abstract void PauseGame();

        public abstract void SaveGame();
    }
}