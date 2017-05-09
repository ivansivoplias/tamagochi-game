using System;
using System.Xml;
using System.Xml.Schema;
using Tamagochi.Common;
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

        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; }
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

        public TimeSpan InnerPetTime
        {
            get { return _innerPetTime; }
            set { _innerPetTime = value; }
        }

        public GameState GameState
        {
            get { return _lastGameState; }
            set { _lastGameState = value; }
        }

        public bool IsDefault => _isDefault;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            _gameTime = TimeSpan.Parse(reader.GetAttribute("GameTime"));
            _innerPetTime = TimeSpan.Parse(reader.GetAttribute("InnerPetTime"));
            _age = float.Parse(reader.GetAttribute("Age"));
            _mood = float.Parse(reader.GetAttribute("Mood"));
            _satiety = float.Parse(reader.GetAttribute("Satiety"));
            _health = float.Parse(reader.GetAttribute("Health"));
            _cleanessRate = float.Parse(reader.GetAttribute("CleanessRate"));

            Enum.TryParse(reader.GetAttribute("GameState"), out _lastGameState);
            Enum.TryParse(reader.GetAttribute("PetType"), out _petType);
            Enum.TryParse(reader.GetAttribute("EvolutionLevel"), out _evolutionLevel);

            bool isEmptyElement = reader.IsEmptyElement;
            reader.ReadStartElement();
            if (!isEmptyElement)
            {
                reader.ReadEndElement();
            }
        }

        public void Save()
        {
            if (_serializer != null)
            {
                _serializer.Serialize(this);
            }
            else
            {
                throw new Exception();
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("GameTime", _gameTime.ToString());
            writer.WriteAttributeString("InnerPetTime", _innerPetTime.ToString());
            writer.WriteAttributeString("Age", _age.ToString());
            writer.WriteAttributeString("Mood", _mood.ToString());
            writer.WriteAttributeString("Satiety", _satiety.ToString());
            writer.WriteAttributeString("Health", _health.ToString());
            writer.WriteAttributeString("CleanessRate", _cleanessRate.ToString());

            writer.WriteAttributeString("GameState", _lastGameState.ToString());
            writer.WriteAttributeString("PetType", _petType.ToString());
            writer.WriteAttributeString("EvolutionLevel", _evolutionLevel.ToString());
        }
    }
}