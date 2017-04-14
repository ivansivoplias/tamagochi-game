using System;

namespace Tamagochi.Abstract
{
    public interface IAviary
    {
        float CleannessRate { get; }

        void ChangeCleannessRate(float cleannessRateDifference);

        event EventHandler<EventArgs> CleannessCriticalLevelCrossed;
    }
}