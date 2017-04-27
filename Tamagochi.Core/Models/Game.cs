using Tamagochi.Common;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class Game : AbstractGame
    {
        public Game(IGameContext context, AbstractTamagochiTimer timer, IPet pet) : base(context)
        {
            Timer = timer;
            Pet = pet;
        }

        public override void CleanPetAviary()
        {
            //aviary cleaness
            var petCleaness = Pet.CleanessRate;
            Pet.CleanessRate = IncreaseByConstant(petCleaness, GameConstants.AviaryGarbageReduceRate);
        }

        public override void EuthanaizePet()
        {
            //kills pet
            Pet.Health = 0;
            Pet.Mood = 0;
            Pet.Satiety = 0;
            Pet.CleanessRate = 0;
        }

        public override void FeedPet()
        {
            //health + satiety
            var petHealth = Pet.Health;
            var satiety = Pet.Satiety;
            Pet.Health = IncreaseByConstant(petHealth, GameConstants.PetHealthIncreaseRate);
            Pet.Satiety = IncreaseByConstant(satiety, GameConstants.PetSatietyIncreaseRate);
        }

        public override void PlayWithPet()
        {
            //mood
            var petMood = Pet.Mood;
            Pet.Mood = IncreaseByConstant(petMood, GameConstants.PetMoodIncreaseRate);
        }

        private float IncreaseByConstant(float startValue, float constant)
        {
            if (startValue < 100)
            {
                startValue += constant;
                if (startValue > 100)
                {
                    startValue = 100;
                }
            }
            return startValue;
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
            Timer.HourChanged += Pet.OnGameHourChanged;
            Timer.YearChanged += Pet.OnGameYearChanged;
        }

        public override void StopGame()
        {
            Timer.StopTimer();
            Timer.HourChanged -= Pet.OnGameHourChanged;
            Timer.YearChanged -= Pet.OnGameYearChanged;
            SaveGame();
        }
    }
}