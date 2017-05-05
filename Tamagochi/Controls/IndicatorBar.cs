using System.Windows;
using System.Windows.Controls;

namespace Tamagochi.UI.Controls
{
    public class IndicatorBar : ProgressBar
    {
        public static readonly DependencyProperty IndicatorMainTextProperty =
            DependencyProperty.Register("IndicatorMainText", typeof(string), typeof(IndicatorBar));

        public string IndicatorMainText
        {
            get { return GetValue(IndicatorMainTextProperty) as string; }
            set { SetValue(IndicatorMainTextProperty, value); }
        }

        static IndicatorBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicatorBar), new FrameworkPropertyMetadata(typeof(IndicatorBar)));
        }

        public IndicatorBar()
        {
            IsIndeterminate = false;
        }
    }
}