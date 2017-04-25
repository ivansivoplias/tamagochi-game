using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Infrastructure.Abstract
{
    public abstract class AbstractGameFactory
    {
        public abstract AbstractGame MakeGame(IGameContext context);
    }
}