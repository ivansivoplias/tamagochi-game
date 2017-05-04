using System;
using Tamagochi.Common;
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
            return _pet;
        }

        public override IPet MakePetFromContext(IGameContext context)
        {
            var lifeDuration = GetPetLifeDuration(context.PetType);

            _pet = new Pet(lifeDuration);
            _pet.CleanessRate = context.CleanessRate;
            _pet.EvolutionLevel = context.EvolutionLevel;

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

                case PetType.Bulbosaur:
                    lifeDuration = 15;
                    break;

                case PetType.Charmander:
                    lifeDuration = 13;
                    break;

                case PetType.Squirtle:
                    lifeDuration = 12;
                    break;
            }
            return lifeDuration;
        }
    }
}