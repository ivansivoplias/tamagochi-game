using System.Windows.Media;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Helpers;

namespace Tamagochi.UI.ViewModels
{
    public class PetViewModel : ViewModelBase
    {
        private IPet _pet;

        public PetViewModel(IPet pet)
        {
            _pet = pet;
            _pet.AgeChanged += (s, e) => OnPropertyChanged(nameof(Age));
            _pet.CleannessChanged += (s, e) => OnPropertyChanged(nameof(AviaryCleanness));
            _pet.MoodChanged += (s, e) => OnPropertyChanged(nameof(Mood));
            _pet.SatietyChanged += (s, e) => OnPropertyChanged(nameof(Satiety));
            _pet.HealthChanged += (s, e) => OnPropertyChanged(nameof(Health));
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
            get { return _pet.CleanessRate; }
            set
            {
                _pet.CleanessRate = value;
                OnPropertyChanged(nameof(AviaryCleanness));
            }
        }

        public ImageSource Image
        {
            get { return ImageSelector.SelectImage(_pet.PetType, _pet.EvolutionLevel); }
        }
    }
}