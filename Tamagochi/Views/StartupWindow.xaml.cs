using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
            _viewModel.SetCloseStartupWindowCallback(this.Close);
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();

            Closing += OnClosing;
        }

        private void PetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedPet = (sender as ListView).SelectedItem as PetItemViewModel;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            _viewModel?.UnregisterCommandsForWindow(this);
        }
    }
}