﻿using System;
using Tamagochi.Abstract;

namespace Tamagochi.Models
{
    public class Aviary : IAviary
    {
        private float _cleannessRate;

        public float CleannessRate => _cleannessRate;

        public Aviary()
        {
            _cleannessRate = 100;
        }

        public void ChangeCleannessRate(float cleannessRateDifference)
        {
            if (_cleannessRate + cleannessRateDifference <= 100)
            {
                _cleannessRate += cleannessRateDifference;
            }
        }

        public void SetCleannessRate(float newCleanness)
        {
            if (newCleanness >= 0 && newCleanness <= 100)
            {
                _cleannessRate = newCleanness;
            }
        }
    }
}