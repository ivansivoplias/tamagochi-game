using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class Game : AbstractGame
    {
        private TimeSpan _gameTime;
        private TimeSpan _innerTime;
        private TimeSpan _realTime;
        private bool IsActive => State == GameState.Active;
        private bool _isContextCreated;

        public override event EventHandler<GameTimeChangedEventArgs> GameTimeChanged;

        public override TimeSpan GameTime
        {
            get
            {
                return _gameTime;
            }

            protected set
            {
                _gameTime = value;
                GameTimeChanged?.Invoke(this, new GameTimeChangedEventArgs(_gameTime));
            }
        }

        public Game(IGameContext context, AbstractTamagochiTimer timer, IPet pet) : base(context, timer)
        {
            Pet = pet;
            if (!context.IsDefault)
            {
                _isContextCreated = true;
            }
            Pet.PetDied += (s, e) =>
            {
                FinishGame();
            };
            State = _context.GameState;
            _gameTime = context.GameTime;
            _innerTime = context.InnerPetTime;
        }

        private void OnRealMinuteChanged(object sender, MinuteChangedEventArgs e)
        {
            PetState state = new PetState(Pet.Mood, Pet.Satiety, Pet.CleanessRate, Pet.Health);
            PetUpdateParams param = PetUpdateUtil.CreateHourParamsFromPetState(state);
            Pet.UpdatePetFromParams(param);
        }

        private void OnRealSecondChanged(object sender, SecondChangedEventArgs e)
        {
            var previosGameTime = _innerTime;
            var previousDate = _innerTime.GetGameDate();
            var seconds = Pet.EvolutionLevel.GetSecondForEvolutionLevel() * GameConstants.DefaultTimeRate;

            GameTime = GameTime.Add(TimeSpan.FromMinutes(1));

            _innerTime = _innerTime.Add(TimeSpan.FromMinutes(seconds));

            var currentDate = _innerTime.GetGameDate();

            if (currentDate.Year > previousDate.Year)
            {
                Pet.IncreaseAge(1);
            }
            SwitchPetEvolutionLevelIfNeeded();
            UpdatePetParams();
        }

        private void UpdatePetParams()
        {
            PetUpdateParams param = PetUpdateUtil.CreateSecondParams();
            Pet.UpdatePetFromParams(param);
        }

        private void SwitchPetEvolutionLevelIfNeeded()
        {
            var maxYearForCurrentEvolution = Pet.EvolutionLevel.GetMaxAgeForEvolutionLevel();
            var date = _innerTime.GetGameDate();
            float year = date.GetFloatYear();

            if (year >= maxYearForCurrentEvolution)
            {
                Pet.EvolutionLevel = Pet.EvolutionLevel.Next();
            }
        }

        #region Game pet affect methods

        public override void CleanPetAviary()
        {
            if (IsActive)
            {
                Pet.ChangeCleaness(GameConstants.AviaryGarbageReduceRate);
            }
        }

        public override void EuthanaizePet()
        {
            if (IsActive)
            {
                //kills pet
                Pet.Health = 0;
                Pet.Mood = 0;
                Pet.Satiety = 0;
                Pet.CleanessRate = 0;
            }
        }

        public override void FeedPet()
        {
            if (IsActive)
            {
                Pet.UpdateHealth(GameConstants.PetHealthIncreaseRate);
                Pet.ChangeSatiety(GameConstants.PetSatietyIncreaseRate);
            }
        }

        public override void PlayWithPet()
        {
            if (IsActive)
            {
                Pet.UpdateMood(GameConstants.PetMoodIncreaseRate);
            }
        }

        #endregion Game pet affect methods

        #region Game control methods

        public override void PauseGame()
        {
            if (IsActive)
            {
                State = GameState.Paused;
                _timer.StopTimer();
            }
        }

        public override void SaveGame()
        {
            _context.GameTime = GameTime;
            _context.InnerPetTime = _innerTime;
            _context.Age = Pet.Age;
            _context.CleanessRate = Pet.CleanessRate;
            _context.Health = Pet.Health;
            _context.Mood = Pet.Mood;
            _context.PetType = Pet.PetType;
            _context.Satiety = Pet.Satiety;
            _context.GameState = State;
            _context.EvolutionLevel = Pet.EvolutionLevel;
            _context.Save();
        }

        public override void StartGame()
        {
            if (_timer.State != TimerState.Active && State != GameState.Finished) _timer.StartTimer();
            _realTime = _timer.RealTime;
            if (State == GameState.Default || _isContextCreated)
            {
                _timer.RealMinuteChanged += OnRealMinuteChanged;
                _timer.RealSecondChanged += OnRealSecondChanged;
                _isContextCreated = false;
            }
            State = GameState.Active;
        }

        public override void StopGame()
        {
            if (IsActive || State == GameState.Paused)
            {
                State = GameState.Stopped;
                StopTimerAndUnsubscribe();
                SaveGame();
            }
        }

        private void StopTimerAndUnsubscribe()
        {
            _timer.StopTimer();
            _timer.RealMinuteChanged -= OnRealMinuteChanged;
            _timer.RealSecondChanged -= OnRealSecondChanged;
        }

        private void FinishGame()
        {
            State = GameState.Finished;
            StopTimerAndUnsubscribe();
            SaveGame();
        }

        #endregion Game control methods
    }
}