using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface IGameContextProvider
    {
        IGameContext GetGameContext(ISerializer serializer, ISettings settings);
    }
}