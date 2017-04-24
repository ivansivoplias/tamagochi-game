using System;
using System.Xml;
using System.Xml.Schema;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Models
{
    public class GameContext : IGameContext
    {
        private TimeSpan _gameTime;
        private int _age;
        private float _mood;
        private float _satiety;
        private float _health;
        private PetType _petType;
        private float _cleanessRate;
        private ISerializer _serializer;

        public GameContext(ISerializer serializer, string fileName)
        {
            _serializer.Initialize(fileName);
            _serializer = serializer;
        }

        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public float Mood
        {
            get { return _mood; }
            set { _mood = value; }
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

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            _gameTime = TimeSpan.Parse(reader.GetAttribute("GameTime"));
            _age = int.Parse(reader.GetAttribute("Age"));
            _mood = float.Parse(reader.GetAttribute("Mood"));
            _satiety = float.Parse(reader.GetAttribute("Satiety"));
            _health = float.Parse(reader.GetAttribute("Health"));
            _cleanessRate = float.Parse(reader.GetAttribute("CleanessRate"));

            Enum.TryParse(reader.GetAttribute("PetType"), out _petType);

            bool isEmptyElement = reader.IsEmptyElement;
            reader.ReadStartElement();
            if (!isEmptyElement)
            {
                reader.ReadEndElement();
            }
        }

        public void Save()
        {
            _serializer.Serialize(this);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("GameTime", _gameTime.ToString());
            writer.WriteAttributeString("Age", _age.ToString());
            writer.WriteAttributeString("Mood", _mood.ToString());
            writer.WriteAttributeString("Satiety", _satiety.ToString());
            writer.WriteAttributeString("Health", _health.ToString());
            writer.WriteAttributeString("CleanessRate", _cleanessRate.ToString());

            writer.WriteAttributeString("PetType", _petType.ToString());
        }
    }
}