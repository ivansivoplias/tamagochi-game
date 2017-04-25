using System;
using System.Windows;
using Tamagochi.Commands;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private AbstractGame _game;
        private Command _startGameCommand;
        private Command _stopGameCommand;
        private Command _pauseGameCommand;
        private Command _saveGameCommand;

        public Command StartGameCommand => _startGameCommand;
        public Command StopGameCommand => _stopGameCommand;
        public Command PauseGameCommand => _pauseGameCommand;
        public Command SaveGameCommand => _saveGameCommand;

        public TimeSpan Time
        {
            get { return _game.Timer.CurrentTime; }
        }

        public GameViewModel(AbstractGame game)
        {
            _game = game;

            var currentType = GetType();

            _startGameCommand = new Command("Start", "StartGame", currentType, null, _game.StartGame);
            _stopGameCommand = new Command("Stop", "StopGame", currentType, null, _game.StopGame);
            _pauseGameCommand = new Command("Pause", "PauseGame", currentType, null, _game.PauseGame);
            _saveGameCommand = new Command("Save", "SaveGame", currentType, null, _game.SaveGame);
        }

        public void RegisterCommands(Window window)
        {
            Command.RegisterCommandBinding(window, _startGameCommand);
            Command.RegisterCommandBinding(window, _saveGameCommand);
            Command.RegisterCommandBinding(window, _pauseGameCommand);
            Command.RegisterCommandBinding(window, _stopGameCommand);
        }

        private void ShowMessage()
        {
            MessageBox.Show("This is Sparta!");
        }
    }
}