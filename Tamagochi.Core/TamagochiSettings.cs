using System.Configuration;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Common
{
    public class TamagochiSettings : ISettings
    {
        private readonly ApplicationSettingsBase _settings;

        private string _isFirstRunPropName;

        public bool IsFirstRun
        {
            get { return GetValue<bool>(_isFirstRunPropName); }
            set { SetValue(_isFirstRunPropName, value); }
        }

        public TamagochiSettings(ApplicationSettingsBase settings, string isFirstRun)
        {
            _settings = settings;
            _isFirstRunPropName = isFirstRun;
        }

        protected T GetValue<T>(string propName)
        {
            return (T)_settings[propName];
        }

        protected void SetValue(string propName, object value)
        {
            _settings[propName] = value;
            _settings.Save();
        }

        public void SaveSettings()
        {
            _settings.Save();
        }
    }
}