using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractGame
    {
        protected IGameContext _context;
        protected AbstractTamagochiTimer _timer;

        public AbstractGame(IGameContext context, AbstractTamagochiTimer timer)
        {
            _context = context;
            _timer = timer;
        }

        public abstract TimeSpan GameTime { get; protected set; }

        public abstract event EventHandler<GameTimeChangedEventArgs> GameTimeChanged;

        public IPet Pet { get; protected set; }

        public GameState State { get; protected set; }

        public abstract bool IsActive { get; }

        public abstract bool IsStopped { get; }

        public abstract bool IsPaused { get; }

        public abstract bool IsFinished { get; }

        public abstract void FeedPet();

        public abstract void PlayWithPet();

        public abstract void CleanPetAviary();

        public abstract void EuthanaizePet();

        public abstract void StartGame();

        public abstract void StopGame();

        public abstract void PauseGame();

        public abstract void SaveGame();

        public abstract void RestartGame();
    }
}