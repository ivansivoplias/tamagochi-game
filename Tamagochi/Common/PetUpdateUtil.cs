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
            if (petState.Satiety < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 10;
                petParams.MoodDifference -= 5;
            }
        }

        private static void ConfigureParamsFromMood(PetUpdateParams petParams, PetState petState)
        {
            if (petState.Mood < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 5;
            }
        }

        private static void ConfigureParamsFromAviaryCleanness(PetUpdateParams petParams, PetState petState)
        {
            if (petState.AviaryCleannessRate < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 10;
                petParams.MoodDifference -= 15;
            }
        }
    }
}