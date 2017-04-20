using Tamagochi.Common.GameEventArgs;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IPet
    {
        int Age { get; set; }
        int LifeDuration { get; }
        float Mood { get; set; }
        float Satiety { get; set; }
        float Health { get; set; }
        string ImagePath { get; set; }
        PetType PetType { get; set; }
        IAviary Aviary { get; set; }

        void IncreaseAge(int amount);

        void UpdateMood(float moodDifference);

        void ChangeSatiety(float satietyDifference);

        void UpdateHealth(float healthDifference);

        void OnGameHourChanged(object sender, HourChangedEventArgs e);

        void OnGameYearChanged(object sender, YearChangedEventArgs e);
    }
}