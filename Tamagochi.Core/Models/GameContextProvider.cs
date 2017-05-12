using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class GameContextProvider : IGameContextProvider
    {
        public IGameContext GetGameContext(ISerializer serializer, ISettings settings)
        {
            IGameContext context;
            try
            {
                serializer.Initialize(settings.GameContextFilename);
                var tempcontext = serializer.Deserialize<GameContext>();
                tempcontext.SetSerializer(serializer);
                context = tempcontext;
            }
            catch
            {
                context = GameContext.GetDefault(serializer);
            }
            return context;
        }
    }
}