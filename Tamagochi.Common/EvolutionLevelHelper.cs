using System;
using System.Collections.Generic;
using System.Linq;

namespace Tamagochi.Common
{
    public static class EvolutionLevelHelper
    {
        private static IEnumerable<IEvolutionTimePresenter> _converters;

        #region Private Interfaces

        private interface IEvolutionTimePresenter
        {
            int SecondInMinutes { get; }
            float PeriodInGameYears { get; }
            PetEvolutionLevel Level { get; }
        }

        #endregion Private Interfaces

        #region Private Structures

        private struct AdultTime : IEvolutionTimePresenter
        {
            public PetEvolutionLevel Level => PetEvolutionLevel.Adult;

            public float PeriodInGameYears => 6;

            public int SecondInMinutes => 432;
        }

        private struct OlderTeenTime : IEvolutionTimePresenter
        {
            public PetEvolutionLevel Level => PetEvolutionLevel.OlderTeen;

            public float PeriodInGameYears => 3;

            public int SecondInMinutes => 324;
        }

        private struct TeenTime : IEvolutionTimePresenter
        {
            public PetEvolutionLevel Level => PetEvolutionLevel.Teen;

            public float PeriodInGameYears => 3;

            public int SecondInMinutes => 648;
        }

        private struct ChildTime : IEvolutionTimePresenter
        {
            public PetEvolutionLevel Level => PetEvolutionLevel.Child;

            public float PeriodInGameYears => 1.5f;

            public int SecondInMinutes => 324;
        }

        private struct BirthTime : IEvolutionTimePresenter
        {
            public PetEvolutionLevel Level => PetEvolutionLevel.Child;

            public float PeriodInGameYears => 1.5f;

            public int SecondInMinutes => 648;
        }

        #endregion Private Structures

        #region Constructor

        static EvolutionLevelHelper()
        {
            _converters = new List<IEvolutionTimePresenter>()
            {
                new BirthTime(),
                new ChildTime(),
                new TeenTime(),
                new OlderTeenTime(),
                new AdultTime()
            };
        }

        #endregion Constructor

        public static int GetMinuteForEvolutionLevel(this PetEvolutionLevel currentLevel)
        {
            return _converters.First(converter => converter.Level == currentLevel).SecondInMinutes * 60;
        }

        public static float GetYearPeriodForEvolutionLevel(this PetEvolutionLevel currentLevel)
        {
            return _converters.First(converter => converter.Level == currentLevel).PeriodInGameYears;
        }

        public static PetEvolutionLevel Next(this PetEvolutionLevel currentLevel)
        {
            var valuesArray = Enum.GetValues(typeof(PetEvolutionLevel)) as PetEvolutionLevel[];
            int intCurrent = (int)currentLevel;
            if (currentLevel < PetEvolutionLevel.Adult)
            {
                return valuesArray[intCurrent + 1];
            }
            return currentLevel;
        }
    }
}