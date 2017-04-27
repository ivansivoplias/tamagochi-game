﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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