using System;
using Tamagochi.Common;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContext
    {
        TimeSpan GameTime { get; set; }
        TimeSpan InnerPetTime { get; set; }

        float Age { get; set; }
        float Mood { get; set; }

        float Satiety { get; set; }
        float Health { get; set; }

        PetType PetType { get; set; }
        PetEvolutionLevel EvolutionLevel { get; set; }

        float CleanessRate { get; set; }

        GameState GameState { get; set; }

        void Reset();

        bool IsDefault { get; }

        void Save();
    }
}