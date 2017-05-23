using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for GameEndWindow.xaml
    /// </summary>
    public partial class FinishGameWindow : Window
    {
        private ViewModelBase _viewModel;

        public FinishGameWindow(FinishGameViewModel viewModel)
        {
            viewModel.SetRestartGameCallback(RestartGameHandler);
            _viewModel = viewModel;
            this.DataContext = viewModel;
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();

            Closing += OnClosing;
        }

        private void RestartGameHandler(StartupWindowViewModel model)
        {
            var startupWindow = new StartupWindow(model);
            startupWindow.Show();
            this.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _viewModel?.UnregisterCommandsForWindow(this);
        }
    }
}