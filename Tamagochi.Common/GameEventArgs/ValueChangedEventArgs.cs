using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class ValueChangedEventArgs : EventArgs
    {
        public float CurrentValue { get; }

        public ValueChangedEventArgs(float currentValue)
        {
            CurrentValue = currentValue;
        }
    }
}