using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private StartupWindowViewModel _viewModel;

        public StartupWindow(StartupWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.SetStartGameWindowCallback(StartGameWindow);
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();

            Closing += OnClosing;
        }

        private void PetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedPet = (sender as ListView).SelectedItem as PetItemViewModel;
        }

        private void StartGameWindow(AbstractGame game)
        {
            var viewModel = new GameViewModel(game);
            var mainWindow = new MainWindow(viewModel);
            mainWindow.Show();
            this.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _viewModel?.UnregisterCommandsForWindow(this);
        }
    }
}