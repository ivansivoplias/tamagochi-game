using System.Windows.Media;
using Tamagochi.Common;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Helpers;

namespace Tamagochi.UI.ViewModels
{
    public class PetViewModel : ViewModelBase
    {
        private IPet _pet;
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

        public PetViewModel(IPet pet)
        {
            _pet = pet;
            _pet.AgeChanged += (s, e) => OnPropertyChanged(nameof(Age));

            _healthIndicator = new IndicatorViewModel("Health:", GetIndicatorColorFromValue(_pet.Health), (int)_pet.Health);
            _moodIndicator = new IndicatorViewModel("Mood:", GetIndicatorColorFromValue(_pet.Mood), (int)_pet.Mood);
            _satietyIndicator = new IndicatorViewModel("Satiety:", GetIndicatorColorFromValue(_pet.Satiety), (int)_pet.Satiety);
            _cleannessIndicator = new IndicatorViewModel("Aviary cleanness:", GetIndicatorColorFromValue(_pet.CleanessRate), (int)_pet.CleanessRate);

            _pet.CleannessChanged += (s, e) =>
            {
                _cleannessIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
                _cleannessIndicator.Value = (int)e.CurrentValue;
            };
            _pet.MoodChanged += (s, e) =>
            {
                _moodIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
                _moodIndicator.Value = (int)e.CurrentValue;
            };
            _pet.SatietyChanged += (s, e) =>
            {
                _satietyIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
                _satietyIndicator.Value = (int)e.CurrentValue;
            };
            _pet.HealthChanged += (s, e) =>
            {
                _healthIndicator.ForegroundColor = GetIndicatorColorFromValue(e.CurrentValue);
                _healthIndicator.Value = (int)e.CurrentValue;
            };
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

        public ImageSource Image
        {
            get { return ImageSelector.SelectImage(_pet.PetType, _pet.EvolutionLevel); }
        }

        private Brush GetIndicatorColorFromValue(float value)
        {
            if (value <= GameConstants.YellowColorEndLimit)
                return RedBrush;
            if (value <= GameConstants.GreenColorEndLimit)
                return YellowBrush;
            return GreenBrush;
        }
    }
}