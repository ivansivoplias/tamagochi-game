using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Commands;

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

        public GameViewModel(AbstractGame game, EventHandler<EventArgs> petDiedHandler)
        {
            _game = game;
            _petViewModel = new PetViewModel(_game.Pet);
            _game.Pet.PetDied += petDiedHandler;
            _game.GameTimeChanged += OnGameTimeChanged;
            _gameTime = game.GameTime;
            _gameDay = game.GameTime.Days;
            _gameHour = game.GameTime.Hours;
            _gameMinutes = game.GameTime.Minutes;

            var currentType = GetType();

            _startGameCommand = Command.CreateAsyncCommand("Start", "StartGame", currentType, StartGameAsync);
            _stopGameCommand = Command.CreateAsyncCommand("Stop", "StopGame", currentType, StopGameAsync);
            _pauseGameCommand = Command.CreateAsyncCommand("Pause", "PauseGame", currentType, PauseGameAsync);
            _saveGameCommand = Command.CreateAsyncCommand("Save", "SaveGame", currentType, SaveGameAsync);
            _feedPetCommand = Command.CreateAsyncCommand("Feed pet", "FeedPet", currentType, FeedPetAsync);
            _playWithPetCommand = Command.CreateAsyncCommand("Play with pet", "PlayWithPet", currentType, PlayWithPetAsync);
            _cleanAviaryCommand = Command.CreateAsyncCommand("Clean aviary", "CleanAviary", currentType, CleanAviaryAsync);
            _euthanizePetCommand = Command.CreateAsyncCommand("Euthanize pet", "EuthanizePet", currentType, EuthanizePetAsync);
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

        private async Task StartGameAsync()
        {
            _game.StartGame();
            await Task.Delay(1);
        }

        private async Task StopGameAsync()
        {
            _game.StopGame();
            await Task.Delay(1);
        }

        private async Task PauseGameAsync()
        {
            _game.PauseGame();
            await Task.Delay(1);
        }

        private async Task SaveGameAsync()
        {
            _game.SaveGame();
            await Task.Delay(1);
        }

        private async Task FeedPetAsync()
        {
            _game.FeedPet();
            await Task.Delay(1);
        }

        private async Task PlayWithPetAsync()
        {
            _game.PlayWithPet();
            await Task.Delay(1);
        }

        private async Task CleanAviaryAsync()
        {
            _game.CleanPetAviary();
            await Task.Delay(1);
        }

        private async Task EuthanizePetAsync()
        {
            _game.EuthanaizePet();
            await Task.Delay(1);
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