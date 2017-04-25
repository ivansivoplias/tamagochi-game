using System;
using System.Windows;
using Tamagochi.Commands;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private AbstractGame _game;
        private TimeSpan _gameTime;
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
            get { return _gameTime; }
            private set
            {
                _gameTime = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public GameViewModel(AbstractGame game)
        {
            _game = game;
            _game.Timer.HourChanged += UpdateTimeOnHourChanged;

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

        private void UpdateTimeOnHourChanged(object sender, HourChangedEventArgs e)
        {
            Time = e.CurrentGameTime;
        }

        private void ShowMessage()
        {
            MessageBox.Show("This is Sparta!");
        }
    }
}