using System.Configuration;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISettingsProvider
    {
        ISettings GetSettings(ApplicationSettingsBase settings);
    }
}