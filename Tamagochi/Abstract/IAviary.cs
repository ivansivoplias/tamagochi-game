using System;

namespace Tamagochi.Abstract
{
    public interface IAviary
    {
        float CleannessRate { get; }

        void ChangeCleannessRate(float cleannessRateDifference);

        void SetCleannessRate(float newCleanness);
    }
}