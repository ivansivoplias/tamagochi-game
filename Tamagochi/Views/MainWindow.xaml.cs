using System.Windows;
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

        public MainWindow(GameViewModel viewModel)
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.RegisterCommandsForWindow(this);

            InitializeComponent();

            fullScreenMode.Click += ToggleFullScreenMode;
            windowMode.Click += ToggleFullScreenMode;

            _isInFullScreenMode = false;
            _windowStartPosition = new Point(0, 0);
        }

        public void ToggleFullScreenMode(object sender, RoutedEventArgs e)
        {
            if (!_isInFullScreenMode && sender == fullScreenMode)
            {
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