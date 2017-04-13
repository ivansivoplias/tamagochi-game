using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tamagochi.Abstract;

namespace Tamagochi.Common.GameExceptions
{
    public class PetIsDeadException : ApplicationException
    {
        public IPet Pet { get; }

        public PetIsDeadException(IPet pet)
        {
            Pet = pet;
        }
    }
}