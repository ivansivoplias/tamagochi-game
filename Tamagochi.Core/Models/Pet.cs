using System;
using Tamagochi.Infrastructure.Abstract;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Core.Models
{
    public class Pet : IPet
    {
        private int _age;
        private int _lifeDuration;
        private float _moodLevel;
        private float _satietyLevel;
        private float _healthLevel;
        private float _cleanessLevel;

        public event EventHandler<ValueChangedEventArgs> SatietyChanged;

        public event EventHandler<ValueChangedEventArgs> CleannessChanged;

        public event EventHandler<ValueChangedEventArgs> HealthChanged;

        public event EventHandler<ValueChangedEventArgs> MoodChanged;

        public event EventHandler<PetDiedEventArgs> PetDied;

        public int Age
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
            get { return _moodLevel; }
            set { SetSatiety(value); }
        }

        public float Health
        {
            get { return _moodLevel; }
            set { SetHealth(value); }
        }

        public float CleanessRate
        {
            get { return _cleanessLevel; }

            set { SetCleaness(value); }
        }

        public string ImagePath { get; set; }
        public PetType PetType { get; set; }

        public Pet(int lifeDuration)
        {
            _lifeDuration = lifeDuration;
            _age = 0;
            _moodLevel = _satietyLevel = _healthLevel = _cleanessLevel = 100;
        }

        public void ChangeSatiety(float satietyDifference)
        {
            if (CheckIfDifferenceIsValid(satietyDifference, _satietyLevel))
            {
                _satietyLevel += satietyDifference;
                SatietyChanged?.Invoke(this, new ValueChangedEventArgs(Satiety));
            }
        }

        public void IncreaseAge(int amount)
        {
            if (amount > 0 && amount < _lifeDuration && _age + amount <= _lifeDuration)
            {
                _age += amount;
            }

            if (Age == _lifeDuration)
            {
                PetDied?.Invoke(this, PetDiedEventArgs.Default);
            }
        }

        public void UpdateHealth(float healthDifference)
        {
            if (CheckIfDifferenceIsValid(healthDifference, _healthLevel))
            {
                _healthLevel += healthDifference;
                HealthChanged?.Invoke(this, new ValueChangedEventArgs(Health));
            }

            if (Health == GameConstants.HelthDeathLimit)
            {
                PetDied?.Invoke(this, PetDiedEventArgs.Default);
            }
        }

        public void UpdateMood(float moodDifference)
        {
            if (CheckIfDifferenceIsValid(moodDifference, _moodLevel))
            {
                _moodLevel += moodDifference;
                MoodChanged?.Invoke(this, new ValueChangedEventArgs(Mood));
            }
        }

        public void ChangeCleaness(float cleanessDifference)
        {
            if (CheckIfDifferenceIsValid(cleanessDifference, _cleanessLevel))
            {
                _cleanessLevel += cleanessDifference;
                CleannessChanged?.Invoke(this, new ValueChangedEventArgs(CleanessRate));
            }
        }

        public void OnGameHourChanged(object sender, HourChangedEventArgs e)
        {
            PetState state = new PetState(Mood, Satiety, CleanessRate);
            PetUpdateParams param = PetUpdateUtil.CreateFromPetState(state);
            UpdatePetFromParams(param);
        }

        public void OnGameYearChanged(object sender, YearChangedEventArgs e)
        {
            IncreaseAge(1);
        }

        public bool CheckIfDifferenceIsValid(float difference, float currentValue)
        {
            return CheckIfNumberIsPercent(Math.Abs(difference))
                && Math.Abs(difference) <= currentValue
                && currentValue + difference <= 100;
        }

        private void UpdatePetFromParams(PetUpdateParams parameter)
        {
            UpdateHealth(parameter.HealthDifference);
            UpdateMood(parameter.MoodDifference);
        }

        private void SetAge(int newAge)
        {
            if (newAge > _age)
            {
                _age = newAge;
            }

            if (Age == _lifeDuration)
            {
                PetDied?.Invoke(this, PetDiedEventArgs.Default);
            }
        }

        private void SetSatiety(float newSatiety)
        {
            if (CheckIfNumberIsPercent(newSatiety))
            {
                _satietyLevel = newSatiety;
                SatietyChanged?.Invoke(this, new ValueChangedEventArgs(Satiety));
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
                HealthChanged?.Invoke(this, new ValueChangedEventArgs(Health));
            }

            if (Health == GameConstants.HelthDeathLimit)
            {
                PetDied?.Invoke(this, PetDiedEventArgs.Default);
            }
        }

        private void SetMood(float newMood)
        {
            if (CheckIfNumberIsPercent(newMood))
            {
                _moodLevel = newMood;
                MoodChanged?.Invoke(this, new ValueChangedEventArgs(Mood));
            }
        }

        private void SetCleaness(float newCleaness)
        {
            if (CheckIfNumberIsPercent(newCleaness))
            {
                _cleanessLevel = newCleaness;
                CleannessChanged?.Invoke(this, new ValueChangedEventArgs(CleanessRate));
            }
        }
    }
}