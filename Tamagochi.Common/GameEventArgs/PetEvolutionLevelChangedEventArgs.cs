using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class PetEvolutionLevelChangedEventArgs : EventArgs
    {
        public PetEvolutionLevel Current { get; }

        public PetEvolutionLevelChangedEventArgs(PetEvolutionLevel level)
        {
            Current = level;
        }
    }
}