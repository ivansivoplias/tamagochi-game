using System;

namespace Tamagochi.Abstract
{
    public interface ISettings
    {
        bool IsFirstRun { get; set; }

        IPet Pet { get; set; }

        TimeSpan GameTime { get; set; }

        void SaveSettings();
    }
}