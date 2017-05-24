using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel _viewModel;
        private IPet _pet;
        private bool _isInFullScreenMode;
        private Point _windowStartPosition;
        private Size _startSize;
        private WindowState _startState;
        private Image _activeIcon;

        public MainWindow(GameViewModel viewModel)
        {
            _pet = viewModel.GetPet();
            viewModel.GameFinishedMessage += FinishGameMessageHandler;
            viewModel.NewGameMessage += NewGameMessageHandler;
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.RegisterCommandsForWindow(this);

            InitializeComponent();

            Closing += OnClosing;

            fullScreenMode.Click += ToggleFullScreenMode;
            windowMode.Click += ToggleFullScreenMode;

            _isInFullScreenMode = false;
            _windowStartPosition = new Point(0, 0);

            _activeIcon = new Image();

            var bitmapSource = new BitmapImage(new Uri("pack://application:,,,/Images/Icons/active.png"));
            bitmapSource.Freeze();
            _activeIcon.Source = bitmapSource;
            _activeIcon.Width = 15;
            _activeIcon.Height = 15;

            windowMode.Icon = _activeIcon;
        }

        private void FinishGameMessageHandler(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var model = new FinishGameViewModel(_pet);
                var finishGameWindow = new FinishGameWindow(model);
                finishGameWindow.Show();
                this.Close();
            });
        }

        private void NewGameMessageHandler(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var startupWindow = new StartupWindow(new StartupWindowViewModel());
                startupWindow.Show();
                this.Close();
            });
        }

        public void ToggleFullScreenMode(object sender, RoutedEventArgs e)
        {
            if (!_isInFullScreenMode && sender == fullScreenMode)
            {
                windowMode.Icon = null;
                fullScreenMode.Icon = _activeIcon;

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

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Closing -= OnClosing;
            _viewModel?.UnregisterCommandsForWindow(this);
        }

        ~MainWindow()
        {
            _viewModel.NewGameMessage -= NewGameMessageHandler;
            _viewModel.GameFinishedMessage -= FinishGameMessageHandler;
            fullScreenMode.Click -= ToggleFullScreenMode;
            windowMode.Click -= ToggleFullScreenMode;
        }
    }
}