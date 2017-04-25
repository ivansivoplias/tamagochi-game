using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tamagochi.Infrastructure.Abstract
{
    public interface ISettingsProvider
    {
        ISettings GetSettings(ApplicationSettingsBase settings);
    }
}