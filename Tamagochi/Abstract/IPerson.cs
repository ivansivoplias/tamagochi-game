using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Abstract
{
    public interface IPerson
    {
        IPet Pet { get; set; }

        void FeedPet();

        void PlayWithPet();

        void CleanAviary();

        void KillPet();
    }
}