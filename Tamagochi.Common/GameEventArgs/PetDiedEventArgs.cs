using System;

namespace Tamagochi.Common.GameEventArgs
{
    public class PetDiedEventArgs : EventArgs
    {
        public static PetDiedEventArgs Default = new PetDiedEventArgs();
    }
}