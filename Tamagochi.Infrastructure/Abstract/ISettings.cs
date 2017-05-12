namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISettings
    {
        bool IsFirstRun { get; set; }

        string GameContextFilename { get; set; }

        void SaveSettings();
    }
}