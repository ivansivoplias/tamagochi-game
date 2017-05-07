using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class Game : AbstractGame
    {
        private TimeSpan _gameTime;
        private TimeSpan _realTime;

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
            _gameTime = context.GameTime;
        }

        private void OnRealMinuteChanged(object sender, MinuteChangedEventArgs e)
        {
            var previosGameTime = GameTime;
            var previousDate = new DateTime(previosGameTime.Ticks);
            var minutes = Pet.EvolutionLevel.GetMinuteForEvolutionLevel();

            _gameTime = _gameTime.Add(TimeSpan.FromMinutes(minutes));

            var currentDate = new DateTime(GameTime.Ticks);
            if (currentDate.Year > previousDate.Year)
            {
                Pet.IncreaseAge(1);
            }
            SwitchPetEvolutionLevelIfNeeded();
            UpdatePetParams();
        }

        private void UpdatePetParams()
        {
            PetState state = new PetState(Pet.Mood, Pet.Satiety, Pet.CleanessRate, Pet.Health);
            PetUpdateParams param = PetUpdateUtil.CreateFromPetState(state);
            Pet.UpdatePetFromParams(param);
        }

        private void SwitchPetEvolutionLevelIfNeeded()
        {
            var maxYearForCurrentEvolution = Pet.EvolutionLevel.GetYearPeriodForEvolutionLevel();
            var date = new DateTime(GameTime.Ticks);
            if (date.Year >= maxYearForCurrentEvolution)
            {
                Pet.EvolutionLevel = Pet.EvolutionLevel.Next();
            }
        }

        #region Game pet affect methods

        public override void CleanPetAviary()
        {
            //aviary cleaness
            var petCleaness = Pet.CleanessRate;
            Pet.CleanessRate = IncreaseByConstant(petCleaness, GameConstants.AviaryGarbageReduceRate);
        }

        public override void EuthanaizePet()
        {
            //kills pet
            Pet.Health = 0;
            Pet.Mood = 0;
            Pet.Satiety = 0;
            Pet.CleanessRate = 0;
        }

        public override void FeedPet()
        {
            //health + satiety
            var petHealth = Pet.Health;
            var satiety = Pet.Satiety;
            Pet.Health = IncreaseByConstant(petHealth, GameConstants.PetHealthIncreaseRate);
            Pet.Satiety = IncreaseByConstant(satiety, GameConstants.PetSatietyIncreaseRate);
        }

        public override void PlayWithPet()
        {
            //mood
            var petMood = Pet.Mood;
            Pet.Mood = IncreaseByConstant(petMood, GameConstants.PetMoodIncreaseRate);
        }

        private float IncreaseByConstant(float startValue, float constant)
        {
            if (startValue < 100)
            {
                startValue += constant;
                if (startValue > 100)
                {
                    startValue = 100;
                }
            }
            return startValue;
        }

        #endregion Game pet affect methods

        #region Game control methods

        public override void PauseGame()
        {
            _timer.StopTimer();
        }

        public override void SaveGame()
        {
            _context.GameTime = GameTime;
            _context.Age = Pet.Age;
            _context.CleanessRate = Pet.CleanessRate;
            _context.Health = Pet.Health;
            _context.Mood = Pet.Mood;
            _context.PetType = Pet.PetType;
            _context.Satiety = Pet.Satiety;
            _context.Save();
        }

        public override void StartGame()
        {
            _timer.StartTimer();
            _realTime = _timer.RealTime;
            _timer.RealMinuteChanged += OnRealMinuteChanged;
            //TODO: add pet time event handlers
        }

        public override void StopGame()
        {
            _timer.StopTimer();
            _timer.RealMinuteChanged -= OnRealMinuteChanged;
            //TODO: remove pet time event handlers
            SaveGame();
        }

        #endregion Game control methods
    }
}