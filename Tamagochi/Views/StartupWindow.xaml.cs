using Autofac;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        private StartupWindowViewModel _viewModel;

        public StartupWindow(StartupWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.PetSelectedMessage += SelectPetMessageHandler;
            this.DataContext = _viewModel;
            _viewModel.RegisterCommandsForWindow(this);
            InitializeComponent();

            Closing += OnClosing;
        }

        private void PetsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.SelectedPet = (sender as ListView).SelectedItem as PetItemViewModel;
        }

        private void SelectPetMessageHandler(object sender, EventArgs e)
        {
            if (_viewModel.HasSelectedPet)
            {
                var petType = (PetType)Enum.Parse(typeof(PetType), _viewModel.SelectedPet.PetName);

                var gameContext = App.Container.Resolve<IGameContext>();
                gameContext.Reset();
                gameContext.PetType = petType;
                var game = App.Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var viewModel = new GameViewModel(game);
                    var mainWindow = new MainWindow(viewModel);
                    mainWindow.Show();
                    this.Close();
                });
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Closing -= OnClosing;
            _viewModel.Pets.Clear();
            PetsList.SelectionChanged -= PetsList_SelectionChanged;
            _viewModel.PetSelectedMessage -= SelectPetMessageHandler;
            _viewModel?.UnregisterCommandsForWindow(this);
        }
    }
}