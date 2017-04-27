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
        private Command _feedPetCommand;
        private Command _playWithPetCommand;
        private Command _euthanizePetCommand;
        private Command _cleanAviaryCommand;
        private PetViewModel _petViewModel;

        public Command StartGameCommand => _startGameCommand;
        public Command StopGameCommand => _stopGameCommand;
        public Command PauseGameCommand => _pauseGameCommand;
        public Command SaveGameCommand => _saveGameCommand;
        public Command FeedPetCommand => _feedPetCommand;
        public Command PlayWithPetCommand => _playWithPetCommand;
        public Command CleanAviaryCommand => _cleanAviaryCommand;
        public Command EuthanizePet => _euthanizePetCommand;

        public PetViewModel Pet
        {
            get { return _petViewModel; }
            protected set { _petViewModel = value; }
        }

        public TimeSpan Time
        {
            get { return _gameTime; }
            private set
            {
                _gameTime = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public GameViewModel(AbstractGame game, EventHandler<PetDiedEventArgs> petDiedHandler)
        {
            _game = game;
            _petViewModel = new PetViewModel(_game.Pet);
            _game.Pet.PetDied += petDiedHandler;
            _game.Timer.HourChanged += UpdateTimeOnHourChanged;

            var currentType = GetType();

            _startGameCommand = new Command("Start", "StartGame", currentType, null, _game.StartGame);
            _stopGameCommand = new Command("Stop", "StopGame", currentType, null, _game.StopGame);
            _pauseGameCommand = new Command("Pause", "PauseGame", currentType, null, _game.PauseGame);
            _saveGameCommand = new Command("Save", "SaveGame", currentType, null, _game.SaveGame);
            _feedPetCommand = new Command("Feed pet", "FeedPet", currentType, null, _game.FeedPet);
            _playWithPetCommand = new Command("Play with pet", "PlayWithPet", currentType, null, _game.PlayWithPet);
            _cleanAviaryCommand = new Command("Clean aviary", "CleanAviary", currentType, null, _game.CleanPetAviary);
            _euthanizePetCommand = new Command("Euthanize pet", "EuthanizePet", currentType, null, _game.EuthanaizePet);
        }

        public override void RegisterCommandsForWindow(Window window)
        {
            Command.RegisterCommandBinding(window, _startGameCommand);
            Command.RegisterCommandBinding(window, _saveGameCommand);
            Command.RegisterCommandBinding(window, _pauseGameCommand);
            Command.RegisterCommandBinding(window, _stopGameCommand);
            Command.RegisterCommandBinding(window, _cleanAviaryCommand);
            Command.RegisterCommandBinding(window, _feedPetCommand);
            Command.RegisterCommandBinding(window, _playWithPetCommand);
            Command.RegisterCommandBinding(window, _euthanizePetCommand);
        }

        private void UpdateTimeOnHourChanged(object sender, HourChangedEventArgs e)
        {
            Time = e.CurrentGameTime;
        }
    }
}