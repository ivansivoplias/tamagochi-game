namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContextProvider
    {
        IGameContext GetGameContext(ISerializer serializer, ISettings settings);
    }
}