namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISerializer
    {
        void Initialize(string fileName);

        void Serialize<T>(T item);

        T Deserialize<T>();
    }
}