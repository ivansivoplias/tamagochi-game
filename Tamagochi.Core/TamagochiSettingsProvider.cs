using System.Configuration;
using Tamagochi.Common;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core
{
    public class TamagochiSettingsProvider : ISettingsProvider
    {
        public ISettings GetSettings(ApplicationSettingsBase settings)
        {
            return new TamagochiSettings(settings);
        }
    }
}