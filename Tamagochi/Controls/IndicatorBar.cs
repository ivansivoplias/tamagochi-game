using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Tamagochi.UI.Controls
{
    public class IndicatorBar : ProgressBar
    {
        public static readonly DependencyProperty IndicatorNameProperty =
            DependencyProperty.Register(nameof(IndicatorName), typeof(string), typeof(IndicatorBar));

        public static readonly DependencyProperty PercentColorProperty =
            DependencyProperty.Register(nameof(PercentColor), typeof(Brush), typeof(IndicatorBar));

        public string IndicatorName
        {
            get { return GetValue(IndicatorNameProperty) as string; }
            set { SetValue(IndicatorNameProperty, value); }
        }

        public Brush PercentColor
        {
            get { return GetValue(PercentColorProperty) as Brush; }
            set { SetValue(PercentColorProperty, value); }
        }

        static IndicatorBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IndicatorBar), new FrameworkPropertyMetadata(typeof(IndicatorBar)));
        }

        public IndicatorBar()
        {
            IsIndeterminate = false;
            Loaded += OnLoadedAnimationHandler;
        }

        private void OnLoadedAnimationHandler(object sender, RoutedEventArgs e)
        {
            if (IsIndeterminate)
                AnimateIndetermined();
        }

        protected virtual void AnimateIndetermined()
        {
            var storyboard = GetTemplateChild("indeterminateStoryboard") as Storyboard;

            var rootGrid = (Grid)GetTemplateChild("TemplateRoot");

            var thicknessAnimation = (storyboard.Children[1] as ThicknessAnimation);
            thicknessAnimation.To = new Thickness(ActualWidth + 100, 0, 0, 0);
            storyboard.Begin(rootGrid);
        }
    }
}