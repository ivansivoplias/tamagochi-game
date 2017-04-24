using System;
using System.Xml.Serialization;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContext : IXmlSerializable
    {
        TimeSpan GameTime { get; set; }

        int Age { get; set; }
        float Mood { get; set; }

        float Satiety { get; set; }
        float Health { get; set; }

        PetType PetType { get; set; }

        float CleanessRate { get; set; }

        void Save();
    }
}