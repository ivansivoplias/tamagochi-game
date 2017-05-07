namespace Tamagochi.Common
{
    public struct PetState
    {
        public float Health { get; }
        public float Mood { get; }
        public float Satiety { get; }
        public float AviaryCleannessRate { get; }

        public PetState(float mood, float satiety, float cleannessRate, float health)
        {
            Mood = mood;
            Satiety = satiety;
            AviaryCleannessRate = cleannessRate;
            Health = health;
        }
    }
}