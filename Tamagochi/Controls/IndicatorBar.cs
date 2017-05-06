using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
            Loaded += (s, e) =>
            {
                if (IsIndeterminate) AnimateIndetermined();
            };
        }

        protected virtual void AnimateIndetermined()
        {
            var storyboard = new Storyboard();

            var rootGrid = (Grid)GetTemplateChild("TemplateRoot");

            var thicknessAnimation = new ThicknessAnimation()
            {
                AccelerationRatio = 0.1,
                DecelerationRatio = 0.5,
                Duration = TimeSpan.FromSeconds(1.5),
                RepeatBehavior = RepeatBehavior.Forever,
                From = new Thickness(-200, 0, 0, 0),
                To = new Thickness(ActualWidth + 100, 0, 0, 0)
            };
            Storyboard.SetTargetName(thicknessAnimation, "AnimationBody");
            Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(thicknessAnimation);
            storyboard.Begin(rootGrid);
        }
    }
}