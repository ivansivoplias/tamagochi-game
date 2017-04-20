using System;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISettings
    {
        bool IsFirstRun { get; set; }

        void SaveSettings();
    }
}