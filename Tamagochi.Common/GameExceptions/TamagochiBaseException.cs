using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Common.GameExceptions
{
    public class TamagochiBaseException : Exception
    {
        public string ClassName { get; }
        public const string DefaultMessage = "An unexpectable error occurred. Please save it and show administrator.";

        public TamagochiBaseException(string className) : this(className, DefaultMessage)
        {
        }

        public TamagochiBaseException(string className, string message) : base(message)
        {
            ClassName = className;
        }
    }
}