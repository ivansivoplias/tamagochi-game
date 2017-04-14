using System;

namespace Tamagochi.Abstract
{
    public interface ISettings
    {
        bool IsFirstRun { get; set; }

        void SaveSettings();
    }
}