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
        private Action _restartCallback;
        private Command _closeAppCommand;
        private Command _restartGameCommand;

        public ICommand CloseAppCommand => _closeAppCommand;
        public ICommand RestartGameCommand => _restartGameCommand;

        public PetStatisticsViewModel Pet => _petViewModel;

        public FinishGameViewModel(IPet pet)
        {
            _pet = pet;
            _petViewModel = new PetStatisticsViewModel(_pet);
            _closeAppCommand = Command.CreateCommand("Close app", "CloseApp", GetType(), () => Application.Current.Shutdown());
            _restartGameCommand = Command.CreateCommand("Restart game", "RestartGame", GetType(), RestartGameCommandExecute);
        }

        public void SetRestartGameCallback(Action restartCallback)
        {
            _restartCallback = restartCallback;
        }

        private void RestartGameCommandExecute()
        {
            Application.Current.Dispatcher.Invoke(RestartGame);
        }

        private void RestartGame()
        {
            _restartCallback?.Invoke();
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