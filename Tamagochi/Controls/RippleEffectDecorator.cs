using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tamagochi.UI.Controls
{
    public class RippleEffectDecorator : ContentControl
    {
        private Ellipse _ellipse;
        private Grid _grid;
        private Storyboard _animation;
        private ContentPresenter _content;

        // Using a DependencyProperty as the backing store for HighlightBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HighlightBackgroundProperty =
            DependencyProperty.Register("HighlightBackground", typeof(Brush), typeof(RippleEffectDecorator), new PropertyMetadata(Brushes.White));

        static RippleEffectDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RippleEffectDecorator), new FrameworkPropertyMetadata(typeof(RippleEffectDecorator)));
        }

        public Brush HighlightBackground
        {
            get { return (Brush)GetValue(HighlightBackgroundProperty); }
            set { SetValue(HighlightBackgroundProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AnimateRippleEffect();
        }

        private void AnimateRippleEffect()
        {
            _ellipse = GetTemplateChild("PART_ellipse") as Ellipse;
            _content = GetTemplateChild("PART_contentpresenter") as ContentPresenter;
            Panel.SetZIndex(_ellipse, 1000);
            _grid = GetTemplateChild("RippleEffectRoot") as Grid;
            _grid.SizeChanged += (s, e) =>
            {
                if (e.WidthChanged)
                {
                    _content.Width = e.NewSize.Width;
                }
            };

            _animation = _grid.FindResource("PART_animation") as Storyboard;

            this.AddHandler(MouseDownEvent, new RoutedEventHandler((sender, e) =>
            {
                var targetHeight = Math.Max(ActualWidth, ActualHeight);
                var mousePosition = (e as MouseButtonEventArgs).GetPosition(this);

                var startMargin = new Thickness(mousePosition.X, mousePosition.Y, 0, 0);

                //set initial margin to mouse position
                _ellipse.Margin = startMargin;

                //set the to value of the animation that animates the width to the target width
                (_animation.Children[0] as DoubleAnimation).To = targetHeight;

                //set the to and from values of the animation that animates the distance relative to the container (grid)
                (_animation.Children[1] as ThicknessAnimation).From = startMargin;
                (_animation.Children[1] as ThicknessAnimation).To =
                        new Thickness(mousePosition.X - targetHeight / 2, mousePosition.Y - targetHeight / 2, 0, 0);
                _ellipse.BeginStoryboard(_animation);
            }), true);
        }
    }
}