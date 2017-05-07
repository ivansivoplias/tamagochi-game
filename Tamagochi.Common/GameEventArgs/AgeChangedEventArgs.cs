using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class AgeChangedEventArgs : EventArgs
    {
        public int PreviousAge { get; }
        public int NewAge { get; }

        public AgeChangedEventArgs(int previousAge, int newAge)
        {
            PreviousAge = previousAge;
            NewAge = newAge;
        }
    }
}