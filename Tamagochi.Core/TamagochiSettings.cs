using System;
using System.Configuration;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Common
{
    public class TamagochiSettings : ISettings
    {
        private readonly ApplicationSettingsBase _settings;

        private string _isFirstRunPropName;
        private string _contextFileNamePropName;

        public bool IsFirstRun
        {
            get { return GetValue<bool>(_isFirstRunPropName); }
            set { SetValue(_isFirstRunPropName, value); }
        }

        public string GameContextFilename
        {
            get { return GetValue<string>(_contextFileNamePropName); }
            set { SetValue(_contextFileNamePropName, value); }
        }

        public TamagochiSettings(ApplicationSettingsBase settings)
        {
            _settings = settings;
            _isFirstRunPropName = nameof(IsFirstRun);
            _contextFileNamePropName = nameof(GameContextFilename);
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