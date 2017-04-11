namespace Tamagochi.Abstract
{
    public abstract class AbstractPetFactory
    {
        public abstract IPet CreatePet(PetType petType);
    }
}