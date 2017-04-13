using System;
using Tamagochi.Abstract;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Models
{
    public class Pet : IPet
    {
        private int _age;
        private readonly int _lifeDuration;
        private float _moodLevel;
        private float _satietyLevel;
        private float _healthLevel;

        public int Age => _age;

        public int LifeDuration => _lifeDuration;

        public float Mood => _moodLevel;

        public float Satiety => _satietyLevel;

        public float Health => _healthLevel;

        public string ImagePath { get; set; }
        public PetType PetType { get; set; }
        public IAviary Aviary { get; set; }

        public Pet(int age, int lifeDuration, float moodLevel, float satietyLevel, float healthLevel)
        {
            _age = age;
            _lifeDuration = lifeDuration;
            _moodLevel = moodLevel;
            _satietyLevel = satietyLevel;
            _healthLevel = healthLevel;
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

        public void OnGameHourChanged(object sender, HourChangedEventArgs e)
        {
            PetState state = new PetState(Mood, Satiety, Aviary.CleannessRate);
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
    }
}