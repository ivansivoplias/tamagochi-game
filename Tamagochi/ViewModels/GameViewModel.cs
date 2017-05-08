using System;
using System.Windows;
using Tamagochi.UI.Commands;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;
using System.Windows.Input;

namespace Tamagochi.UI.ViewModels
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

        private int _gameDay;
        private int _gameHour;
        private int _gameMinutes;

        public ICommand StartGameCommand => _startGameCommand;
        public ICommand StopGameCommand => _stopGameCommand;
        public ICommand PauseGameCommand => _pauseGameCommand;
        public ICommand SaveGameCommand => _saveGameCommand;
        public ICommand FeedPetCommand => _feedPetCommand;
        public ICommand PlayWithPetCommand => _playWithPetCommand;
        public ICommand CleanAviaryCommand => _cleanAviaryCommand;
        public ICommand EuthanizePetCommand => _euthanizePetCommand;

        public PetViewModel Pet
        {
            get { return _petViewModel; }
            protected set { _petViewModel = value; }
        }

        public int GameDay
        {
            get { return _gameDay; }
            set
            {
                _gameDay = value;
                OnPropertyChanged(nameof(GameDay));
            }
        }

        public int GameHour
        {
            get { return _gameHour; }
            set
            {
                _gameHour = value;
                OnPropertyChanged(nameof(GameHour));
            }
        }

        public int GameMinutes
        {
            get { return _gameMinutes; }
            set
            {
                _gameMinutes = value;
                OnPropertyChanged(nameof(GameMinutes));
            }
        }

        public string GameTimeText => "Game time:";

        public string PetAgeText => "Pet age:";

        public GameViewModel(AbstractGame game, EventHandler<PetDiedEventArgs> petDiedHandler)
        {
            _game = game;
            _petViewModel = new PetViewModel(_game.Pet);
            _game.Pet.PetDied += petDiedHandler;
            _game.GameTimeChanged += OnGameTimeChanged;
            _gameTime = game.GameTime;

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

        private void OnGameTimeChanged(object sender, GameTimeChangedEventArgs e)
        {
            _gameTime = e.CurrentGameTime;
            GameDay = _gameTime.Days;
            GameHour = _gameTime.Hours;
            GameMinutes = _gameTime.Minutes;
        }
    }
}