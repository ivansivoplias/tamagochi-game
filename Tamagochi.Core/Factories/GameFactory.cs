using Tamagochi.Common;
using Tamagochi.Core.Models;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Factories
{
    public class GameFactory : AbstractGameFactory
    {
        private AbstractTamagochiTimer _timer;
        private AbstractPetFactory _factory;

        public GameFactory(AbstractPetFactory factory, AbstractTamagochiTimer timer)
        {
            _factory = factory;
            _timer = timer;
        }

        public override AbstractGame MakeGame(IGameContext context)
        {
            IPet pet;
            if (!context.IsDefault && context.GameState != GameState.Finished)
            {
                pet = _factory.MakePetFromContext(context);
            }
            else
            {
                pet = _factory.MakePet(context.PetType);
            }
            return new Game(context, _timer, pet);
        }
    }
}