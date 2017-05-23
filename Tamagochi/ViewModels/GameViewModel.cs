using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.UI.Commands;
using Tamagochi.UI.Helpers;

namespace Tamagochi.UI.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private AbstractGame _game;
        private TimeSpan _gameTime;
        private Command _startGameCommand;
        private Command _restartGameCommand;
        private Command _stopGameCommand;
        private Command _pauseGameCommand;
        private Command _saveGameCommand;
        private Command _feedPetCommand;
        private Command _playWithPetCommand;
        private Command _euthanizePetCommand;
        private Command _cleanAviaryCommand;

        private Command _exitCommand;
        private Command _closeCommand;
        private PetViewModel _petViewModel;

        private Action<FinishGameViewModel> _finishGameCallback;

        private int _gameDay;
        private int _gameHour;
        private int _gameMinutes;

        public ICommand StartGameCommand => _startGameCommand;
        public ICommand RestartGameCommand => _restartGameCommand;
        public ICommand StopGameCommand => _stopGameCommand;
        public ICommand PauseGameCommand => _pauseGameCommand;
        public ICommand SaveGameCommand => _saveGameCommand;
        public ICommand FeedPetCommand => _feedPetCommand;
        public ICommand PlayWithPetCommand => _playWithPetCommand;
        public ICommand CleanAviaryCommand => _cleanAviaryCommand;
        public ICommand EuthanizePetCommand => _euthanizePetCommand;
        public ICommand ExitCommand => _exitCommand;
        public ICommand CloseCommand => _closeCommand;

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

        public ImageSource FeedPetImage { get { return IconsSelector.SelectIconForAction(ActionType.Feed); } }

        public ImageSource PlayWithPetImage { get { return IconsSelector.SelectIconForAction(ActionType.Play); } }

        public ImageSource CleanAviaryImage { get { return IconsSelector.SelectIconForAction(ActionType.Clean); } }

        public ImageSource EuthanizePetImage { get { return IconsSelector.SelectIconForAction(ActionType.Euthanize); } }

        public GameViewModel(AbstractGame game)
        {
            _game = game;
            _petViewModel = new PetViewModel(_game.Pet);
            _game.GameTimeChanged += OnGameTimeChanged;
            _game.Pet.PetDied += PetDiedHandler;
            Application.Current.Exit += (s, e) => _game.StopGame();
            _gameTime = game.GameTime;
            _gameDay = game.GameTime.Days;
            _gameHour = game.GameTime.Hours;
            _gameMinutes = game.GameTime.Minutes;

            var currentType = GetType();

            _startGameCommand = Command.CreateAsyncCommand("Start", "StartGame", currentType, StartGameAsync, () => !_game.IsActive);
            _restartGameCommand = Command.CreateAsyncCommand("Restart", "RestartGame", currentType, RestartGameAsync);
            _stopGameCommand = Command.CreateAsyncCommand("Stop", "StopGame", currentType, StopGameAsync, () => !(_game.IsStopped || _game.IsFinished));
            _pauseGameCommand = Command.CreateAsyncCommand("Pause", "PauseGame", currentType, PauseGameAsync, () => _game.IsActive);
            _saveGameCommand = Command.CreateAsyncCommand("Save", "SaveGame", currentType, SaveGameAsync);
            _feedPetCommand = Command.CreateAsyncCommand("Feed pet", "FeedPet", currentType, FeedPetAsync, () => _game.IsActive);
            _playWithPetCommand = Command.CreateAsyncCommand("Play with pet", "PlayWithPet", currentType, PlayWithPetAsync, () => _game.IsActive);
            _cleanAviaryCommand = Command.CreateAsyncCommand("Clean aviary", "CleanAviary", currentType, CleanAviaryAsync, () => _game.IsActive);
            _euthanizePetCommand = Command.CreateAsyncCommand("Euthanize pet", "EuthanizePet", currentType, EuthanizePetAsync, () => _game.IsActive);
            _exitCommand = Command.CreateCommand("Exit", "ExitCommand", currentType, Exit);
            _closeCommand = Command.CreateCommand("Close", "CloseCommand", currentType, _game.StopGame);
        }

        public override void RegisterCommandsForWindow(Window window)
        {
            Command.RegisterCommandBinding(window, _startGameCommand);
            Command.RegisterCommandBinding(window, _restartGameCommand);
            Command.RegisterCommandBinding(window, _saveGameCommand);
            Command.RegisterCommandBinding(window, _pauseGameCommand);
            Command.RegisterCommandBinding(window, _stopGameCommand);
            Command.RegisterCommandBinding(window, _cleanAviaryCommand);
            Command.RegisterCommandBinding(window, _feedPetCommand);
            Command.RegisterCommandBinding(window, _playWithPetCommand);
            Command.RegisterCommandBinding(window, _euthanizePetCommand);
            Command.RegisterCommandBinding(window, _exitCommand);
            Command.RegisterCommandBinding(window, _closeCommand);
        }

        public override void UnregisterCommandsForWindow(Window window)
        {
            Command.UnregisterCommandBinding(window, _startGameCommand);
            Command.UnregisterCommandBinding(window, _restartGameCommand);
            Command.UnregisterCommandBinding(window, _saveGameCommand);
            Command.UnregisterCommandBinding(window, _pauseGameCommand);
            Command.UnregisterCommandBinding(window, _stopGameCommand);
            Command.UnregisterCommandBinding(window, _cleanAviaryCommand);
            Command.UnregisterCommandBinding(window, _feedPetCommand);
            Command.UnregisterCommandBinding(window, _playWithPetCommand);
            Command.UnregisterCommandBinding(window, _euthanizePetCommand);
            Command.UnregisterCommandBinding(window, _exitCommand);
            Command.UnregisterCommandBinding(window, _closeCommand);
        }

        public void SetFinishGameCallback(Action<FinishGameViewModel> finishGameCallback)
        {
            _finishGameCallback = finishGameCallback;
        }

        private void Exit()
        {
            _game.StopGame();
            Application.Current.Shutdown();
        }

        private void PetDiedHandler(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(InvokeFinishGameCallback);
        }

        private void InvokeFinishGameCallback()
        {
            var finishGameViewModel = new FinishGameViewModel(_game.Pet);
            _finishGameCallback?.Invoke(finishGameViewModel);
        }

        private async Task StartGameAsync()
        {
            _game.StartGame();
            await Task.Delay(1);
        }

        private async Task RestartGameAsync()
        {
            _game.RestartGame();
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

        ~GameViewModel()
        {
            _game.Pet.PetDied -= PetDiedHandler;
            _game.GameTimeChanged -= OnGameTimeChanged;
        }
    }
}