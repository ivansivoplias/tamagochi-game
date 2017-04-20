using System.Xml.Serialization;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISerializer
    {
        void Initialize(string fileName);

        void Serialize<T>(T item) where T : IXmlSerializable;

        T Deserialize<T>() where T : IXmlSerializable;
    }
}