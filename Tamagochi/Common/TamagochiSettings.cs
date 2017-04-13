using System;
using System.Configuration;
using Tamagochi.Abstract;

namespace Tamagochi.Common
{
    public class TamagochiSettings : ISettings
    {
        private readonly ApplicationSettingsBase _settings;

        private string _isFirstRunPropName;
        private string _petPropName;
        private string _gameTimePropName;

        public bool IsFirstRun
        {
            get { return GetValue<bool>(_isFirstRunPropName); }
            set { SetValue(_isFirstRunPropName, value); }
        }

        public IPet Pet
        {
            get { return GetValue<IPet>(_petPropName); }
            set { SetValue(_petPropName, value); }
        }

        public TimeSpan GameTime
        {
            get { return GetValue<TimeSpan>(_gameTimePropName); }
            set { SetValue(_gameTimePropName, value); }
        }

        public TamagochiSettings(ApplicationSettingsBase settings, string isFirstRun, string pet, string gameTime)
        {
            _settings = settings;
            _petPropName = pet;
            _gameTimePropName = gameTime;
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