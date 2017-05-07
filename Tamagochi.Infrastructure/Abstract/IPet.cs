﻿using System;
using Tamagochi.Common;
using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IPet
    {
        int Age { get; set; }
        int LifeDuration { get; }
        float Mood { get; set; }
        float Satiety { get; set; }
        float Health { get; set; }
        PetType PetType { get; set; }
        PetEvolutionLevel EvolutionLevel { get; set; }
        float CleanessRate { get; set; }

        event EventHandler<ValueChangedEventArgs> SatietyChanged;

        event EventHandler<ValueChangedEventArgs> CleannessChanged;

        event EventHandler<ValueChangedEventArgs> HealthChanged;

        event EventHandler<ValueChangedEventArgs> MoodChanged;

        event EventHandler<AgeChangedEventArgs> AgeChanged;

        event EventHandler<PetEvolutionLevelChangedEventArgs> EvolutionLevelChanged;

        event EventHandler<PetDiedEventArgs> PetDied;

        void IncreaseAge(int amount);

        void UpdateMood(float moodDifference);

        void ChangeSatiety(float satietyDifference);

        void ChangeCleaness(float cleanessDifference);

        void UpdateHealth(float healthDifference);

        void UpdatePetFromParams(PetUpdateParams parameter);
    }
}