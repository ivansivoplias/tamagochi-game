using Autofac;
using System;
using System.Windows;
using Tamagochi.Common;
using Tamagochi.DI;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.Properties;
using Tamagochi.UI.ViewModels;
using Tamagochi.UI.Views;

namespace Tamagochi.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; protected set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            SetupContainer();

            var gameContext = Container.Resolve<IGameContext>();

            if (!gameContext.IsDefault
                && gameContext.GameState != GameState.Finished
                && gameContext.PetType != PetType.None)
            {
                var game = Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);
                var viewModel = new GameViewModel(game);
                this.MainWindow = new MainWindow(viewModel);
            }
            else
            {
                this.MainWindow = new StartupWindow(new StartupWindowViewModel());
            }

            this.MainWindow.Show();
        }

        private void SetupContainer()
        {
            var container = new ContainerBuilder();
            AutofacInitializer.Initialize(container, Settings.Default);

            Container = container.Build();
        }
    }
}