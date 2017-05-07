using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class AgeChangedEventArgs : EventArgs
    {
        public float PreviousAge { get; }
        public float NewAge { get; }

        public AgeChangedEventArgs(float previousAge, float newAge)
        {
            PreviousAge = previousAge;
            NewAge = newAge;
        }
    }
}