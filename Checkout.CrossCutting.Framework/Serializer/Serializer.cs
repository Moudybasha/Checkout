using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Checkout.CrossCutting.Core.Serializer;

namespace Checkout.CrossCutting.Framework.Serializer
{
    public class Serializer : ISerializer
    {
        public string XmlSerialize(object objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());
            var stringWriter = new StringWriter();
          
            xmlSerializer.Serialize(stringWriter, objectToSerialize);

            return stringWriter.ToString();
        }

        //another method
//        //public static string Serialize<T>(T value)
//        //{

//        //    if (value == null)
//        //    {
//        //        return null;
//        //    }

//        //    var serializer = new XmlSerializer(typeof(T));

//        //    var settings = new XmlWriterSettings
//        //    {
//        //        Encoding = new UnicodeEncoding(false, false),
//        //        Indent = false,
//        //        OmitXmlDeclaration = false
//        //    };
//        //    // no BOM in a .NET string

//        //    using (StringWriter textWriter = new StringWriter())
//        //    {
//        //        using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
//        //        {
//        //            serializer.Serialize(xmlWriter, value);
//        //        }
//        //        return textWriter.ToString();
//        //    }
//        //}

        public T XmlDeserialize<T>(string xml)
        {
            var serializer = new XmlSerializer(typeof(T));

            var settings = new XmlReaderSettings();
            
            using (var textReader = new StringReader(xml))
            {
                using (var xmlReader = XmlReader.Create(textReader, settings))
                {
                    return (T)serializer.Deserialize(xmlReader);
                }
            }
        }


//        public static byte[] ObjectToByteArray(Object obj)
//        {
//            BinaryFormatter bf = new BinaryFormatter();
//            using (var ms = new MemoryStream())
//            {
//                bf.Serialize(ms, obj);
//                return ms.ToArray();
//            }
//        }

//        public static Object ByteArrayToObject(byte[] arrBytes)
//        {
//            using (var memStream = new MemoryStream())
//            {
//                var binForm = new BinaryFormatter();
//                memStream.Write(arrBytes, 0, arrBytes.Length);
//                memStream.Seek(0, SeekOrigin.Begin);
//                var obj = binForm.Deserialize(memStream);
//                return obj;
//            }
    }
}