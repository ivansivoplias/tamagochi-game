using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.ViewModels
{
    public class PetViewModel : ViewModelBase
    {
        private IPet _pet;
        private IAviary _aviary;

        public PetViewModel(IPet pet)
        {
            _pet = pet;
            _aviary = pet.Aviary;
        }

        public int Age
        {
            get { return _pet.Age; }
            set
            {
                _pet.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public float Mood
        {
            get { return _pet.Mood; }
            set
            {
                _pet.Mood = value;
                OnPropertyChanged(nameof(Mood));
            }
        }

        public float Satiety
        {
            get { return _pet.Satiety; }
            set
            {
                _pet.Satiety = value;
                OnPropertyChanged(nameof(Satiety));
            }
        }

        public float Health
        {
            get { return _pet.Health; }
            set
            {
                _pet.Health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        public float AviaryCleanness
        {
            get { return _aviary.CleannessRate; }
            set
            {
                _aviary.SetCleannessRate(value);
                OnPropertyChanged(nameof(AviaryCleanness));
            }
        }

        public string Image { get; set; }
    }
}