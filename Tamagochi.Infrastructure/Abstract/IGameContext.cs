using System.Xml.Serialization;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContext : IXmlSerializable
    {
        T GetProperty<T>(string propertyName);

        void SetProperty<T>(string propertyName, T property);
    }
}