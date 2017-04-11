using System;
using Tamagochi.Abstract;

namespace Tamagochi.Models
{
    public class PetFactory : AbstractPetFactory
    {
        private int _age;
        private int _lifeDuration;
        private float _moodLevel;
        private float _satietyLevel;
        private float _healthLevel;
        private string _imagePath;
        private PetType _petType;
        private IAviary _aviary;
        private Pet _pet;

        public override IPet CreatePet(PetType petType)
        {
            SetupPetDefaultParamsWithPetType(petType);
            SetupPetLifeDuration(petType);
            SetupImage(petType);
            SetupAviary(petType);
            return Build();
        }

        private void SetupPetDefaultParamsWithPetType(PetType petType)
        {
            _age = 0;
            _moodLevel = 100;
            _healthLevel = 100;
            _satietyLevel = 100;
            _petType = petType;
        }

        private void SetupPetLifeDuration(PetType type)
        {
            switch (type)
            {
                case PetType.None:
                    throw new ArgumentException("Invalid pet type. Life duration cannot be set up.");

                case PetType.Cat:
                    _lifeDuration = 15;
                    break;

                case PetType.Dog:
                    _lifeDuration = 13;
                    break;

                case PetType.Alien:
                    _lifeDuration = 12;
                    break;
            }
        }

        private void SetupImage(PetType type)
        {
            _imagePath = string.Empty;
            switch (type)
            {
                case PetType.None:
                    throw new ArgumentException("Invalid pet type. Image does not exist for pet type None.");

                case PetType.Cat:
                    break;

                case PetType.Dog:
                    break;

                case PetType.Alien:
                    break;
            }
        }

        private void SetupAviary(PetType type)
        {
            _aviary = null;
            switch (type)
            {
                case PetType.None:
                    throw new ArgumentException("Invalid pet type. Aviary for pet type None cannot be found.");

                case PetType.Cat:
                    break;

                case PetType.Dog:
                    break;

                case PetType.Alien:
                    break;
            }
        }

        private Pet Build()
        {
            _pet = new Pet(_age, _lifeDuration, _moodLevel, _satietyLevel, _healthLevel);
            _pet.PetType = _petType;
            _pet.ImagePath = _imagePath;
            _pet.Aviary = _aviary;
            return _pet;
        }
    }
}