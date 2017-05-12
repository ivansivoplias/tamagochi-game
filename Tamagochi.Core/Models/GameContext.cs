using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using Tamagochi.Common;
using Tamagochi.Common.GameExceptions;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class GameContext : IGameContext
    {
        private TimeSpan _gameTime;
        private TimeSpan _innerPetTime;
        private float _age;
        private float _mood;
        private float _satiety;
        private float _health;
        private PetType _petType;
        private PetEvolutionLevel _evolutionLevel;
        private float _cleanessRate;
        private ISerializer _serializer;
        private bool _isDefault;
        private GameState _lastGameState;

        public GameContext()
        {
        }

        public GameContext(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public void SetSerializer(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public static GameContext GetDefault(ISerializer serializer)
        {
            var context = new GameContext(serializer);
            context._isDefault = true;
            return context;
        }

        [XmlIgnore]
        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; }
        }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "GameTime")]
        public string GameTimeString
        {
            get
            {
                return XmlConvert.ToString(GameTime);
            }
            set
            {
                GameTime = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        public float Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public float Mood
        {
            get { return _mood; }
            set { _mood = value; }
        }

        public PetEvolutionLevel EvolutionLevel
        {
            get { return _evolutionLevel; }
            set { _evolutionLevel = value; }
        }

        public float Satiety
        {
            get { return _satiety; }
            set { _satiety = value; }
        }

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public PetType PetType
        {
            get { return _petType; }
            set { _petType = value; }
        }

        public float CleanessRate
        {
            get { return _cleanessRate; }
            set { _cleanessRate = value; }
        }

        [XmlIgnore]
        public TimeSpan InnerPetTime
        {
            get { return _innerPetTime; }
            set { _innerPetTime = value; }
        }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "InnerPetTime")]
        public string InnerPetTimeString
        {
            get
            {
                return XmlConvert.ToString(InnerPetTime);
            }
            set
            {
                InnerPetTime = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        public GameState GameState
        {
            get { return _lastGameState; }
            set { _lastGameState = value; }
        }

        [XmlIgnore]
        public bool IsDefault => _isDefault;

        public void Reset()
        {
            _isDefault = true;
            GameState = GameState.Default;
            GameTime = new TimeSpan();
            InnerPetTime = new TimeSpan();
            PetType = PetType.None;
            EvolutionLevel = PetEvolutionLevel.Birth;
            Age = 0;
            Mood = 0;
            Satiety = 0;
            Health = 0;
            CleanessRate = 0;
        }

        public void Save()
        {
            if (_serializer != null)
            {
                _serializer.Serialize(this);
            }
            else
            {
                var details = new ExceptionDetails();
                details.CallerMethodName = nameof(Save);
                details.ClassName = nameof(GameContext);
                throw new SaveContextFailedException(details, "Context serialization failed, because serializer is not set.");
            }
        }
    }
}