namespace Tamagochi.Common
{
    public static class PetUpdateUtil
    {
        public static PetUpdateParams CreateFromPetState(PetState state)
        {
            var petParams = new PetUpdateParams();
            ConfigureParamsFromPetState(petParams, state);
            return petParams;
        }

        private static void ConfigureParamsFromPetState(PetUpdateParams petParams, PetState state)
        {
            ConfigureParamsFromSatiety(petParams, state);
            ConfigureParamsFromMood(petParams, state);
            ConfigureParamsFromAviaryCleanness(petParams, state);
        }

        private static void ConfigureParamsFromSatiety(PetUpdateParams petParams, PetState petState)
        {
            if (petState.Satiety * 100 < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 0.1f;
                petParams.MoodDifference -= 0.05f;
            }
        }

        private static void ConfigureParamsFromMood(PetUpdateParams petParams, PetState petState)
        {
            if (petState.Mood < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 0.05f;
            }
        }

        private static void ConfigureParamsFromAviaryCleanness(PetUpdateParams petParams, PetState petState)
        {
            if (petState.AviaryCleannessRate < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 0.1f;
                petParams.MoodDifference -= 0.15f;
            }
        }
    }
}