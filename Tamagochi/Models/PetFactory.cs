using System;
using Tamagochi.Abstract;

namespace Tamagochi.Models
{
    public class PetFactory : AbstractPetFactory
    {
        private Pet _pet;
        private ISettings _settings;

        public override void Initialize(ISettings settings)
        {
            _settings = settings;
        }

        public override IPet MakePet(PetType petType)
        {
            int lifeDuration = GetPetLifeDuration(petType);

            _pet = new Pet(lifeDuration);
            _pet.PetType = petType;
            _pet.ImagePath = GetImageForPetType(petType);
            _pet.Aviary = GetAviaryForPetType(petType);
            return _pet;
        }

        private int GetPetLifeDuration(PetType type)
        {
            var lifeDuration = 0;
            switch (type)
            {
                case PetType.None:
                    throw new ArgumentException("Invalid pet type. Life duration cannot be set up.");

                case PetType.Cat:
                    lifeDuration = 15;
                    break;

                case PetType.Dog:
                    lifeDuration = 13;
                    break;

                case PetType.Alien:
                    lifeDuration = 12;
                    break;
            }
            return lifeDuration;
        }

        private string GetImageForPetType(PetType type)
        {
            string path = string.Empty;
            if (type == PetType.None)
                throw new ArgumentException("Invalid pet type. Image does not exist for pet type None.");
            else
            {
                int index = (int)type - 1;
                path = _settings.PetImages[index];
            }
            return path;
        }

        private IAviary GetAviaryForPetType(PetType type)
        {
            IAviary aviary = null;
            if (type == PetType.None)
                throw new ArgumentException("Invalid pet type. Aviary for pet type None cannot be found.");
            else
            {
            }
            return aviary;
        }
    }
}