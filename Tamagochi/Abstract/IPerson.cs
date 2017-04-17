namespace Tamagochi.Abstract
{
    public interface IPerson
    {
        IPet Pet { get; }

        void FeedPet();

        void PlayWithPet();

        void CleanAviary();

        void KillPet();
    }
}