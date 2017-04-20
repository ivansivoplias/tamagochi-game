namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractGame
    {
        protected ISerializer _serializer;
        protected IGameContext _context;

        public AbstractGame(ISerializer serializer, IGameContext context)
        {
            _serializer = serializer;
            _context = context;
        }

        public AbstractTamagochiTimer Timer { get; }

        public IPerson Person { get; }

        public abstract void StartGame();

        public abstract void StopGame();

        public abstract void PauseGame();

        public abstract void SaveGame();
    }
}