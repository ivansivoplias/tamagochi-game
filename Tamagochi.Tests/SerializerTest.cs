using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using Tamagochi.Core.Models;
using Tamagochi.Core.Serializer;

namespace Tamagochi.Tests
{
    [TestFixture]
    public class SerializerTest
    {
        [Test]
        public void SerializerSerilize_TypeInt_Serialization_Successfull()
        {
            //Arrange
            var serializer = new TamagochiSerializer();
            var filename = GetCurrentDirectory() + "\\success.xml";
            serializer.Initialize(filename);
            var testData = new GameContext() { Age = 25, CleanessRate = 48, Health = 50 };

            //Act
            serializer.Serialize(testData);
            var t = serializer.Deserialize<GameContext>();

            //Assert
            Assert.AreEqual(testData.Age, t.Age);
            Assert.AreEqual(testData.CleanessRate, t.CleanessRate);
            Assert.AreEqual(testData.Health, t.Health);
        }

        public string GetCurrentDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}