using System;
using Tamagochi.Core.Models;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Factories
{
    public class PetFactory : AbstractPetFactory
    {
        private Pet _pet;

        public override IPet MakePet(PetType petType)
        {
            int lifeDuration = GetPetLifeDuration(petType);

            _pet = new Pet(lifeDuration);
            _pet.PetType = petType;
            _pet.ImagePath = GetImageForPetType(petType);
            return _pet;
        }

        public override IPet MakePetFromContext(IGameContext context)
        {
            var lifeDuration = GetPetLifeDuration(context.PetType);

            _pet = new Pet(lifeDuration);
            _pet.ImagePath = GetImageForPetType(context.PetType);
            _pet.CleanessRate = context.CleanessRate;

            _pet.Age = context.Age;
            _pet.Health = context.Health;
            _pet.Mood = context.Mood;
            _pet.PetType = context.PetType;
            _pet.Satiety = context.Satiety;

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
                //TODO: write this stuff
            }
            return path;
        }
    }
}