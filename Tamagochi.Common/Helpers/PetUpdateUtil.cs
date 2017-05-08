namespace Tamagochi.Common
{
    public static class PetUpdateUtil
    {
        public static PetUpdateParams CreateHourParamsFromPetState(PetState state)
        {
            var petParams = new PetUpdateParams();
            ConfigureHourParamsFromPetState(petParams, state);
            return petParams;
        }

        public static PetUpdateParams CreateSecondParams()
        {
            var petParams = new PetUpdateParams();
            ConfigureSecondParams(petParams);
            return petParams;
        }

        private static void ConfigureHourParamsFromPetState(PetUpdateParams petParams, PetState state)
        {
            ConfigureHourParamsFromSatiety(petParams, state);
            ConfigureHourParamsFromMood(petParams, state);
            ConfigureHourParamsFromAviaryCleanness(petParams, state);
            ConfigureParamsFromHealth(petParams);
        }

        private static void ConfigureSecondParams(PetUpdateParams petParams)
        {
            ConfigureParamsFromAviaryCleanness(petParams);
            ConfigureParamsFromHealth(petParams);
            ConfigureParamsFromMood(petParams);
            ConfigureParamsFromSatiety(petParams);
        }

        private static void ConfigureHourParamsFromSatiety(PetUpdateParams petParams, PetState petState)
        {
            if (petState.Satiety < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 10;
                petParams.MoodDifference -= 5;
            }
        }

        private static void ConfigureHourParamsFromMood(PetUpdateParams petParams, PetState petState)
        {
            if (petState.Mood < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 5;
            }
        }

        private static void ConfigureHourParamsFromAviaryCleanness(PetUpdateParams petParams, PetState petState)
        {
            if (petState.AviaryCleannessRate < GameConstants.CriticalLimit)
            {
                petParams.HealthDifference -= 10;
                petParams.MoodDifference -= 15;
            }
        }

        private static void ConfigureParamsFromAviaryCleanness(PetUpdateParams petParams)
        {
            petParams.AviaryCleannessDifference -= GameConstants.AviaryGarbageGrowRate;
        }

        private static void ConfigureParamsFromHealth(PetUpdateParams petParams)
        {
            petParams.HealthDifference -= GameConstants.PetHealthDecreaseRate;
        }

        private static void ConfigureParamsFromSatiety(PetUpdateParams petParams)
        {
            petParams.SatietyDifference -= GameConstants.PetSatietyDecreaseRate;
        }

        private static void ConfigureParamsFromMood(PetUpdateParams petParams)
        {
            petParams.MoodDifference -= GameConstants.PetMoodDecreaseRate;
        }
    }
}