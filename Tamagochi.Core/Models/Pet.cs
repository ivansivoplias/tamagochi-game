using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class Pet : IPet
    {
        private float _age;
        private int _lifeDuration;
        private float _moodLevel;
        private float _satietyLevel;
        private float _healthLevel;
        private float _cleanessLevel;
        private PetEvolutionLevel _evolutionLevel;

        public event EventHandler<ValueChangedEventArgs> SatietyChanged;

        public event EventHandler<ValueChangedEventArgs> CleannessChanged;

        public event EventHandler<ValueChangedEventArgs> HealthChanged;

        public event EventHandler<ValueChangedEventArgs> MoodChanged;

        public event EventHandler<EventArgs> PetDied;

        public event EventHandler<PetEvolutionLevelChangedEventArgs> EvolutionLevelChanged;

        public event EventHandler<AgeChangedEventArgs> AgeChanged;

        public float Age
        {
            get { return _age; }
            set { SetAge(value); }
        }

        public int LifeDuration
        {
            get { return _lifeDuration; }
        }

        public float Mood
        {
            get { return _moodLevel; }
            set { SetMood(value); }
        }

        public float Satiety
        {
            get { return _satietyLevel; }
            set { SetSatiety(value); }
        }

        public float Health
        {
            get { return _healthLevel; }
            set { SetHealth(value); }
        }

        public float CleanessRate
        {
            get { return _cleanessLevel; }

            set { SetCleaness(value); }
        }

        public bool IsDead => _healthLevel == 0 || _age == _lifeDuration;

        public PetType PetType { get; set; }

        public PetEvolutionLevel EvolutionLevel
        {
            get { return _evolutionLevel; }

            set
            {
                SetEvolutionLevel(value);
            }
        }

        public Pet(int lifeDuration)
        {
            _lifeDuration = lifeDuration;
            _age = 0;
            _moodLevel = _satietyLevel = _healthLevel = _cleanessLevel = 100;
            _evolutionLevel = PetEvolutionLevel.Birth;
        }

        public void ChangeSatiety(float satietyDifference)
        {
            if (CheckIfDifferenceIsValid(satietyDifference))
            {
                Satiety = NormalizeValue(Satiety + satietyDifference);
            }
        }

        public void IncreaseAge(float amount)
        {
            if (amount > 0 && amount < _lifeDuration && Age + amount <= _lifeDuration)
            {
                Age += amount;
            }
        }

        public void UpdateHealth(float healthDifference)
        {
            if (CheckIfDifferenceIsValid(healthDifference))
            {
                Health = NormalizeValue(Health + healthDifference);
            }
        }

        public void UpdateMood(float moodDifference)
        {
            if (CheckIfDifferenceIsValid(moodDifference))
            {
                Mood = NormalizeValue(Mood + moodDifference);
            }
        }

        public void ChangeCleaness(float cleanessDifference)
        {
            if (CheckIfDifferenceIsValid(cleanessDifference))
            {
                CleanessRate = NormalizeValue(CleanessRate + cleanessDifference);
            }
        }

        public void ResetPetState()
        {
            var previousAge = Age;
            _age = 0;
            AgeChanged?.Invoke(this, new AgeChangedEventArgs(previousAge, Age));
            Mood = Satiety = Health = CleanessRate = 100;
            _evolutionLevel = PetEvolutionLevel.Birth;
            EvolutionLevelChanged?.Invoke(this, new PetEvolutionLevelChangedEventArgs(_evolutionLevel));
        }

        private float NormalizeValue(float value)
        {
            if (value > 100)
            {
                value = 100;
            }
            if (value < 0)
            {
                value = 0;
            }
            return value;
        }

        public bool CheckIfDifferenceIsValid(float difference)
        {
            return CheckIfNumberIsPercent(Math.Abs(difference));
        }

        public void UpdatePetFromParams(PetUpdateParams parameter)
        {
            UpdateHealth(parameter.HealthDifference);
            UpdateMood(parameter.MoodDifference);
            ChangeCleaness(parameter.AviaryCleannessDifference);
            ChangeSatiety(parameter.SatietyDifference);
        }

        #region Property set methods

        private void SetEvolutionLevel(PetEvolutionLevel newLevel)
        {
            if (EvolutionLevel < newLevel)
            {
                _evolutionLevel = newLevel;
                EvolutionLevelChanged?.Invoke(this, new PetEvolutionLevelChangedEventArgs(newLevel));
            }
        }

        private void SetAge(float newAge)
        {
            if (newAge > _age)
            {
                var previousAge = _age;
                _age = newAge;
                AgeChanged?.Invoke(this, new AgeChangedEventArgs(previousAge, newAge));
            }

            if (Age == _lifeDuration)
            {
                PetDied?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetSatiety(float newSatiety)
        {
            if (CheckIfNumberIsPercent(newSatiety))
            {
                _satietyLevel = newSatiety;
                SatietyChanged?.Invoke(this, new ValueChangedEventArgs(newSatiety));
            }
        }

        private bool CheckIfNumberIsPercent(float value)
        {
            return value >= 0 && value <= 100;
        }

        private void SetHealth(float newHealth)
        {
            if (CheckIfNumberIsPercent(newHealth))
            {
                _healthLevel = newHealth;
                HealthChanged?.Invoke(this, new ValueChangedEventArgs(newHealth));
            }

            if (Health == GameConstants.HelthDeathLimit)
            {
                PetDied?.Invoke(this, EventArgs.Empty);
            }
        }

        private void SetMood(float newMood)
        {
            if (CheckIfNumberIsPercent(newMood))
            {
                _moodLevel = newMood;
                MoodChanged?.Invoke(this, new ValueChangedEventArgs(newMood));
            }
        }

        private void SetCleaness(float newCleaness)
        {
            if (CheckIfNumberIsPercent(newCleaness))
            {
                _cleanessLevel = newCleaness;
                CleannessChanged?.Invoke(this, new ValueChangedEventArgs(newCleaness));
            }
        }

        #endregion Property set methods
    }
}