using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.UI.ViewModels
{
    public class PetStatisticsViewModel : ViewModelBase
    {
        private IPet _pet;

        public int Age => (int)_pet.Age;

        public int Health => (int)_pet.Health;

        public int Mood => (int)_pet.Mood;

        public int Satiety => (int)_pet.Satiety;

        public PetStatisticsViewModel(IPet pet)
        {
            _pet = pet;
        }
    }
}