using System;
using System.IO;
using System.Xml.Serialization;
using Tamagochi.Common;
using Tamagochi.Common.GameExceptions;
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
            try
            {
                using (var stream = new FileStream(_filename, FileMode.Open))
                {
                    item = (T)serializer.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                var details = new ExceptionDetails();
                details.CallerMethodName = nameof(Deserialize);
                details.ClassName = nameof(TamagochiSerializer);
                details.OriginalExceptionMessage = e.Message;
                details.StackTrace = e.StackTrace;
                throw new DeserializationFailedException(details);
            }
            return item;
        }

        public void Initialize(string fileName)
        {
            if (IsPathValidRootedLocal(fileName) && Path.GetExtension(fileName).Equals(".xml", StringComparison.CurrentCultureIgnoreCase))
            {
                _filename = fileName;
            }
            else
            {
                var details = new ExceptionDetails();
                details.CallerMethodName = nameof(Initialize);
                details.ClassName = nameof(TamagochiSerializer);
                throw new InvalidFilePathException(details, _filename);
            }
        }

        public void Serialize<T>(T item) where T : IXmlSerializable
        {
            var serializer = new XmlSerializer(typeof(T));
            try
            {
                using (var stream = new StreamWriter(_filename, false))
                {
                    serializer.Serialize(stream, item);
                }
            }
            catch (Exception e)
            {
                var details = new ExceptionDetails();
                details.CallerMethodName = nameof(Serialize);
                details.ClassName = nameof(TamagochiSerializer);
                details.OriginalExceptionMessage = e.Message;
                details.StackTrace = e.StackTrace;
                throw new SerializationFailedException(details);
            }
        }

        private bool IsPathValidRootedLocal(string pathString)
        {
            Uri pathUri;
            bool isValidUri = Uri.TryCreate(pathString, UriKind.Absolute, out pathUri);

            return isValidUri && pathUri != null && pathUri.IsLoopback;
        }
    }
}