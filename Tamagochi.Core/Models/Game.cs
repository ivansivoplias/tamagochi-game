using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class Game : AbstractGame
    {
        public Game(IGameContext context, AbstractTamagochiTimer timer, AbstractPetFactory factory) : base(context)
        {
            Pet = factory.MakePetFromContext(_context);
            Timer = timer;
        }

        public override void PauseGame()
        {
            Timer.StopTimer();
        }

        public override void SaveGame()
        {
            _context.GameTime = Timer.CurrentTime;
            _context.Age = Pet.Age;
            _context.CleanessRate = Pet.CleanessRate;
            _context.Health = Pet.Health;
            _context.Mood = Pet.Mood;
            _context.PetType = Pet.PetType;
            _context.Satiety = Pet.Satiety;
            _context.Save();
        }

        public override void StartGame()
        {
            Timer.StartTimer();
        }

        public override void StopGame()
        {
            Timer.StopTimer();
            SaveGame();
        }
    }
}