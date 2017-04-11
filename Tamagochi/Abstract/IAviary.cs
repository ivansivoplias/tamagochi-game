using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Abstract
{
    public interface IAviary
    {
        float CleannessRate { get; }

        void ChangeCleannessRate(float cleannessRateDifference);
    }
}