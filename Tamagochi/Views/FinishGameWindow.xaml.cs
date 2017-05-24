using System;
using System.ComponentModel;
using System.Windows;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for GameEndWindow.xaml
    /// </summary>
    public partial class FinishGameWindow : Window
    {
        private FinishGameViewModel _viewModel;

        public FinishGameWindow(FinishGameViewModel viewModel)
        {
            viewModel.RestartGameMessage += RestartGameHandler;
            _viewModel = viewModel;
            this.DataContext = viewModel;
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();

            Closing += OnClosing;
        }

        private void RestartGameHandler(object sender, EventArgs e)
        {
            var startupWindow = new StartupWindow(new StartupWindowViewModel());
            startupWindow.Show();
            this.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Closing -= OnClosing;
            _viewModel.RestartGameMessage -= RestartGameHandler;
            _viewModel?.UnregisterCommandsForWindow(this);
        }
    }
}