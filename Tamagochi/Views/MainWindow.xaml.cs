using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelBase _viewModel;
        private bool _isInFullScreenMode;
        private Point _windowStartPosition;
        private Size _startSize;
        private WindowState _startState;
        private Image _activeIcon;
        //private Brush _highlightedBackground;
        //private Brush _startBackground;
        //private Brush _startBorderBrush;

        public MainWindow(GameViewModel viewModel)
        {
            viewModel.SetFinishGameCallback(FinishGameHandler);
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.RegisterCommandsForWindow(this);

            InitializeComponent();

            fullScreenMode.Click += ToggleFullScreenMode;
            windowMode.Click += ToggleFullScreenMode;

            _isInFullScreenMode = false;
            _windowStartPosition = new Point(0, 0);
            //windowMode.IsChecked = true;

            _activeIcon = new Image();
            _activeIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Icons/active.png"));
            _activeIcon.Width = 15;
            _activeIcon.Height = 15;

            //_highlightedBackground = new SolidColorBrush(Color.FromArgb(191, 147, 197, 255));
            //_startBackground = windowMode.Background;
            //_startBorderBrush = windowMode.BorderBrush;
            //windowMode.Background = _highlightedBackground;
            //windowMode.BorderBrush = _highlightedBackground;
        }

        private void FinishGameHandler(FinishGameViewModel model)
        {
            var finishGameWindow = new FinishGameWindow(model);
            finishGameWindow.Show();
            this.Close();
        }

        public void ToggleFullScreenMode(object sender, RoutedEventArgs e)
        {
            if (!_isInFullScreenMode && sender == fullScreenMode)
            {
                //fullScreenMode.IsChecked = true;
                windowMode.Icon = null;
                fullScreenMode.Icon = _activeIcon;
                //windowMode.IsChecked = false;
                //windowMode.Background = _startBackground;
                //windowMode.BorderBrush = _startBorderBrush;

                //_startBackground = fullScreenMode.Background;

                //fullScreenMode.Background = _highlightedBackground;
                //fullScreenMode.BorderBrush = _highlightedBackground;

                _startSize = new Size(ActualWidth, ActualHeight);
                _windowStartPosition = new Point(Left, Top);
                _startState = WindowState;

                WindowState = WindowState.Normal;

                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
                Left = 0;
                Top = 0;
                Width = SystemParameters.VirtualScreenWidth;
                Height = SystemParameters.VirtualScreenHeight;

                _isInFullScreenMode = true;
            }
            else if (sender == windowMode && _isInFullScreenMode)
            {
                fullScreenMode.Icon = null;
                windowMode.Icon = _activeIcon;
                //windowMode.IsChecked = true;
                //fullScreenMode.IsChecked = false;
                //fullScreenMode.Background = _startBackground;
                //fullScreenMode.BorderBrush = _startBorderBrush;

                //_startBackground = windowMode.Background;
                //_startBorderBrush = windowMode.BorderBrush;

                //windowMode.Background = _highlightedBackground;
                //windowMode.BorderBrush = _highlightedBackground;

                WindowStyle = WindowStyle.SingleBorderWindow;
                ResizeMode = ResizeMode.CanResize;

                WindowState = _startState;
                Left = _windowStartPosition.X;
                Top = _windowStartPosition.Y;
                Width = _startSize.Width;
                Height = _startSize.Height;

                _windowStartPosition = new Point(0, 0);
                _isInFullScreenMode = false;
                _startState = WindowState.Normal;
            }
        }

        ~MainWindow()
        {
            fullScreenMode.Click -= ToggleFullScreenMode;
            windowMode.Click -= ToggleFullScreenMode;
        }
    }
}