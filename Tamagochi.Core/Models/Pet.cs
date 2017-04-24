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
            throw new NotImplementedException();
        }

        public void IncreaseAge(int amount)
        {
            throw new NotImplementedException();
        }

        public void UpdateHealth(float healthDifference)
        {
            throw new NotImplementedException();
        }

        public void UpdateMood(float moodDifference)
        {
            throw new NotImplementedException();
        }

        public void ChangeCleaness(float cleanessDifference)
        {
            _cleanessLevel += cleanessDifference;
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
        }

        private void SetSatiety(float newSatiety)
        {
            if (CheckIfNumberIsPercent(newSatiety))
            {
                _satietyLevel = newSatiety;
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
            }
        }

        private void SetMood(float newMood)
        {
            if (CheckIfNumberIsPercent(newMood))
            {
                _moodLevel = newMood;
            }
        }

        private void SetCleaness(float newCleaness)
        {
            if (CheckIfNumberIsPercent(newCleaness))
            {
                _cleanessLevel = newCleaness;
            }
        }
    }
}