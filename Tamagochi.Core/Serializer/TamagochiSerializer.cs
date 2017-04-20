﻿using System;
using System.IO;
using System.Xml.Serialization;
using Tamagochi.Infrastructure.Abstract;

namespace Tamagochi.Core.Serializer
{
    public class TamagochiSerializer : ISerializer
    {
        private string _filename = string.Empty;

        public T Deserialize<T>() where T : IXmlSerializable
        {
            var serializer = new XmlSerializer(typeof(T));
            T item = default(T);
            using (var stream = new FileStream(_filename, FileMode.Open))
            {
                item = (T)serializer.Deserialize(stream);
            }
            return item;
        }

        public void Initialize(string fileName)
        {
            if (IsPathValidRootedLocal(fileName) && Path.GetExtension(fileName).Equals("xml", StringComparison.CurrentCultureIgnoreCase))
            {
                _filename = fileName;
            }
            else
            {
                throw new Exception();
            }
        }

        public void Serialize<T>(T item) where T : IXmlSerializable
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new StreamWriter(_filename, false))
            {
                serializer.Serialize(stream, item);
            }
        }

        private bool IsPathValidRootedLocal(String pathString)
        {
            Uri pathUri;
            Boolean isValidUri = Uri.TryCreate(pathString, UriKind.Absolute, out pathUri);

            return isValidUri && pathUri != null && pathUri.IsLoopback;
        }
    }
}