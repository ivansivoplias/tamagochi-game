using System;
using System.Windows;
using Tamagochi.UI.Commands;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;
using System.Windows.Input;
using Tamagochi.Common;
using System.Threading.Tasks;

namespace Tamagochi.UI.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private AbstractGame _game;
        private string _gameTimeString;
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

        public string Time
        {
            get { return _gameTimeString; }
            private set
            {
                _gameTimeString = value;
                OnPropertyChanged(nameof(Time));
            }
        }

        public GameViewModel(AbstractGame game, EventHandler<PetDiedEventArgs> petDiedHandler)
        {
            _game = game;
            _petViewModel = new PetViewModel(_game.Pet);
            _game.Pet.PetDied += petDiedHandler;
            _game.GameTimeChanged += OnGameTimeChanged;
            _gameTime = game.GameTime;
            _gameTimeString = GetGameTimeString();

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

        private string GetGameTimeString()
        {
            var date = _gameTime.GetGameDate();
            return string.Format("{0} years {1} months {2} days passed", date.Year, date.Month, date.Day);
        }

        private void OnGameTimeChanged(object sender, GameTimeChangedEventArgs e)
        {
            _gameTime = e.CurrentGameTime;
            Time = GetGameTimeString();
        }
    }
}