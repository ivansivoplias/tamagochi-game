using System;
using System.Windows;
using System.Windows.Input;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Commands;

namespace Tamagochi.UI.ViewModels
{
    public class FinishGameViewModel : ViewModelBase
    {
        private IPet _pet;
        private PetStatisticsViewModel _petViewModel;
        private Command _closeAppCommand;
        private Command _restartGameCommand;

        public ICommand CloseAppCommand => _closeAppCommand;
        public ICommand RestartGameCommand => _restartGameCommand;

        public event EventHandler RestartGameMessage = delegate { };

        public PetStatisticsViewModel Pet => _petViewModel;

        public FinishGameViewModel(IPet pet)
        {
            _pet = pet;
            _petViewModel = new PetStatisticsViewModel(_pet);
            _closeAppCommand = Command.CreateCommand("Close app", "CloseApp", GetType(), () => Application.Current.Shutdown());
            _restartGameCommand = Command.CreateCommand("Restart game", "RestartGame", GetType(), RestartGame);
        }

        private void RestartGame()
        {
            RestartGameMessage.Invoke(null, EventArgs.Empty);
        }

        public override void RegisterCommandsForWindow(Window window)
        {
            Command.RegisterCommandBinding(window, _closeAppCommand);
            Command.RegisterCommandBinding(window, _restartGameCommand);
        }

        public override void UnregisterCommandsForWindow(Window window)
        {
            Command.UnregisterCommandBinding(window, _closeAppCommand);
            Command.UnregisterCommandBinding(window, _restartGameCommand);
        }
    }
}