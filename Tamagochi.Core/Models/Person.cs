using Tamagochi.Infrastructure.Abstract;
using Tamagochi.Common;

namespace Tamagochi.Core.Models
{
    public class Person : IPerson
    {
        private readonly IPet _pet;

        public Person(IPet pet)
        {
            _pet = pet;
        }

        public IPet Pet => _pet;

        public void CleanAviary()
        {
            Pet.Aviary.ChangeCleannessRate(GameConstants.AviaryGarbageReduceRate);
        }

        public void FeedPet()
        {
            Pet.UpdateHealth(GameConstants.PetHealthIncreaseRate);
            Pet.ChangeSatiety(GameConstants.PetSatietyIncreaseRate);
        }

        public void KillPet()
        {
            Pet.Age = -1;
        }

        public void PlayWithPet()
        {
            Pet.UpdateMood(GameConstants.PetMoodIncreaseRate);
        }
    }
}