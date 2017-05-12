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
            float EvolutionLevelMaxAge { get; }
            PetEvolutionLevel Level { get; }
        }

        #endregion Private Interfaces

        #region Private Structures

        private struct EvolutionTime : IEvolutionTimePresenter
        {
            public float EvolutionLevelMaxAge { get; }

            public PetEvolutionLevel Level { get; }

            public int SecondInMinutes { get; }

            public EvolutionTime(float maxAge, PetEvolutionLevel level, int realSecondInGameMinutes)
            {
                EvolutionLevelMaxAge = maxAge;
                Level = level;
                SecondInMinutes = realSecondInGameMinutes;
            }
        }

        #endregion Private Structures

        #region Constructor

        static EvolutionLevelHelper()
        {
            _converters = new List<IEvolutionTimePresenter>() {
                new EvolutionTime(1.5f, PetEvolutionLevel.Birth, 648),
                new EvolutionTime(3, PetEvolutionLevel.Child, 324),
                new EvolutionTime(6, PetEvolutionLevel.Teen, 648),
                new EvolutionTime(9, PetEvolutionLevel.OlderTeen, 324),
                new EvolutionTime(15, PetEvolutionLevel.Adult, 432)
            };
        }

        #endregion Constructor

        public static int GetMinuteForEvolutionLevel(this PetEvolutionLevel currentLevel)
        {
            return _converters.First(converter => converter.Level == currentLevel).SecondInMinutes * 60;
        }

        public static int GetSecondForEvolutionLevel(this PetEvolutionLevel currentLevel)
        {
            return _converters.First(converter => converter.Level == currentLevel).SecondInMinutes;
        }

        public static float GetMaxAgeForEvolutionLevel(this PetEvolutionLevel currentLevel)
        {
            return _converters.First(converter => converter.Level == currentLevel).EvolutionLevelMaxAge;
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