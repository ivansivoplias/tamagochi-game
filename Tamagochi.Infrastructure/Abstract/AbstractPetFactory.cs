using Tamagochi.Common;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractPetFactory
    {
        public abstract IPet MakePet(PetType petType);

        public abstract IPet MakePetFromContext(IGameContext context);
    }
}