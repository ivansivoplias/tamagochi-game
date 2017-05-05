using Autofac;
using System.Windows;
using Tamagochi.DI;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.Properties;
using Tamagochi.UI.ViewModels;
using Tamagochi.Views;

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
            this.MainWindow = new MainWindow();

            var gameContext = Container.Resolve<IGameContext>();
            var game = Container.Resolve<AbstractGameFactory>().MakeGame(gameContext);

            var viewModel = new GameViewModel(game, (s, arg) => MessageBox.Show("Pet is dead"));
            this.MainWindow.DataContext = viewModel;
            viewModel.RegisterCommandsForWindow(this.MainWindow);
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