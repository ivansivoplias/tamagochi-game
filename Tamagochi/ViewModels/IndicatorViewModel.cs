using System.Windows.Media;

namespace Tamagochi.UI.ViewModels
{
    public class IndicatorViewModel : ViewModelBase
    {
        private string _indicatorName;
        private Brush _colorBrush;
        private int _value;

        public string Name
        {
            get { return _indicatorName; }
            set
            {
                _indicatorName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Brush ForegroundColor
        {
            get { return _colorBrush; }
            set
            {
                _colorBrush = value;
                OnPropertyChanged(nameof(ForegroundColor));
            }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public IndicatorViewModel(string indicatorName, Brush colorBrush, int startvalue)
        {
            Name = indicatorName;
            ForegroundColor = colorBrush;
            Value = startvalue;
        }
    }
}