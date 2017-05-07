using System;
using System.Xml.Serialization;
using Tamagochi.Common;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContext : IXmlSerializable
    {
        TimeSpan GameTime { get; set; }

        float Age { get; set; }
        float Mood { get; set; }

        float Satiety { get; set; }
        float Health { get; set; }

        PetType PetType { get; set; }
        PetEvolutionLevel EvolutionLevel { get; set; }

        float CleanessRate { get; set; }

        bool IsDefault { get; }

        void Save();
    }
}