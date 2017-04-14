namespace Tamagochi.Abstract
{
    public abstract class AbstractPetFactory
    {
        public abstract IPet MakePet(PetType petType);

        public abstract void Initialize(ISettings settings);
    }
}