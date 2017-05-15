using Autofac;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Tamagochi.Common;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.ViewModels;

namespace Tamagochi.UI.Views
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        private ViewModelBase _viewModel;

        public StartupWindow(StartupWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            this.DataContext = _viewModel;
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();
        }

        private void PetsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedPet = (sender as ListView).SelectedItem as PetItemViewModel;
            var petType = (PetType)Enum.Parse(typeof(PetType), selectedPet.PetName);

            var gameContext = App.Container.Resolve<IGameContext>();
            gameContext.Reset();
            gameContext.PetType = petType;
            var game = App.Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);

            var viewModel = new GameViewModel(game, (s, arg) => MessageBox.Show("Pet is dead"));
            var mainWindow = new MainWindow(viewModel);
            mainWindow.Show();
            this.Close();
        }
    }
}