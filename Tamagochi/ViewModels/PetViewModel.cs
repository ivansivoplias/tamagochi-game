using System.Windows.Media;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Helpers;

namespace Tamagochi.UI.ViewModels
{
    public class PetViewModel : ViewModelBase
    {
        private IPet _pet;
        private ImageSource _image;
        private IndicatorViewModel _healthIndicator;
        private IndicatorViewModel _moodIndicator;
        private IndicatorViewModel _satietyIndicator;
        private IndicatorViewModel _cleannessIndicator;
        public static Brush GreenBrush = new SolidColorBrush(Color.FromArgb(90, 51, 140, 71));
        public static Brush RedBrush = new SolidColorBrush(Color.FromArgb(90, 104, 7, 7));
        public static Brush YellowBrush = new SolidColorBrush(Color.FromArgb(90, 193, 189, 58));

        public IndicatorViewModel HealthIndicator
        {
            get
            {
                return _healthIndicator;
            }
            set
            {
                _healthIndicator = value;
                OnPropertyChanged(nameof(HealthIndicator));
            }
        }

        public IndicatorViewModel MoodIndicator
        {
            get
            {
                return _moodIndicator;
            }
            set
            {
                _moodIndicator = value;
                OnPropertyChanged(nameof(MoodIndicator));
            }
        }

        public IndicatorViewModel SatietyIndicator
        {
            get
            {
                return _satietyIndicator;
            }
            set
            {
                _satietyIndicator = value;
                OnPropertyChanged(nameof(SatietyIndicator));
            }
        }

        public IndicatorViewModel CleannessIndicator
        {
            get
            {
                return _cleannessIndicator;
            }
            set
            {
                _cleannessIndicator = value;
                OnPropertyChanged(nameof(CleannessIndicator));
            }
        }

        static PetViewModel()
        {
            GreenBrush = new SolidColorBrush(Color.FromArgb(90, 51, 140, 71));
            RedBrush = new SolidColorBrush(Color.FromArgb(90, 104, 7, 7));
            YellowBrush = new SolidColorBrush(Color.FromArgb(90, 193, 189, 58));

            GreenBrush.Freeze();
            RedBrush.Freeze();
            YellowBrush.Freeze();
        }

        public PetViewModel(IPet pet)
        {
            _pet = pet;
            _pet.AgeChanged += AgeChangedHandler;
            _image = ImageSelector.SelectImage(_pet.PetType, _pet.EvolutionLevel);

            _healthIndicator = new IndicatorViewModel("Health:", GetIndicatorColorFromValue(_pet.Health), (int)_pet.Health);
            _moodIndicator = new IndicatorViewModel("Mood:", GetIndicatorColorFromValue(_pet.Mood), (int)_pet.Mood);
            _satietyIndicator = new IndicatorViewModel("Satiety:", GetIndicatorColorFromValue(_pet.Satiety), (int)_pet.Satiety);
            _cleannessIndicator = new IndicatorViewModel("Aviary cleanness:", GetIndicatorColorFromValue(_pet.CleanessRate), (int)_pet.CleanessRate);

            _pet.EvolutionLevelChanged += EvolutionLevelChangedHandler;

            _pet.CleannessChanged += CleannessChangedHandler;
            _pet.MoodChanged += MoodChangedHandler;
            _pet.SatietyChanged += SatietyChangedHandler;
            _pet.HealthChanged += HealthChangedHandler;
        }

        public float Age
        {
            get { return _pet.Age; }
            set
            {
                _pet.Age = value;
                OnPropertyChanged(nameof(Age));
            }
        }

        public ImageSource Image => _image;

        private Brush GetIndicatorColorFromValue(float value)
        {
            if (value <= GameConstants.YellowColorEndLimit)
                return RedBrush;
            if (value <= GameConstants.GreenColorEndLimit)
                return YellowBrush;
            return GreenBrush;
        }

        private void AgeChangedHandler(object sender, AgeChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Age));
        }

        private void EvolutionLevelChangedHandler(object sender, PetEvolutionLevelChangedEventArgs e)
        {
            _image = ImageSelector.SelectImage(_pet.PetType, _pet.EvolutionLevel);
            OnPropertyChanged(nameof(Image));
        }

        private void CleannessChangedHandler(object sender, ValueChangedEventArgs e)
        {
            _cleannessIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
            _cleannessIndicator.Value = (int)e.CurrentValue;
        }

        private void MoodChangedHandler(object sender, ValueChangedEventArgs e)
        {
            _moodIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
            _moodIndicator.Value = (int)e.CurrentValue;
        }

        private void SatietyChangedHandler(object sender, ValueChangedEventArgs e)
        {
            _satietyIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
            _satietyIndicator.Value = (int)e.CurrentValue;
        }

        private void HealthChangedHandler(object sender, ValueChangedEventArgs e)
        {
            _healthIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
            _healthIndicator.Value = (int)e.CurrentValue;
        }

        ~PetViewModel()
        {
            _pet.AgeChanged -= AgeChangedHandler;
            _pet.EvolutionLevelChanged -= EvolutionLevelChangedHandler;

            _pet.CleannessChanged -= CleannessChangedHandler;
            _pet.MoodChanged -= MoodChangedHandler;
            _pet.SatietyChanged -= SatietyChangedHandler;
            _pet.HealthChanged -= HealthChangedHandler;
        }
    }
}