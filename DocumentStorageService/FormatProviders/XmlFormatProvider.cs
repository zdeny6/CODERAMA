using System.IO;
using System.Xml.Serialization;
using DocumentStorageService.Models;
using DocumentStorageService.Interfaces;
using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;

namespace DocumentStorageService.FormatProviders
{
    public class XmlFormatProvider : IFormatProvider
    {
        public string Format => "application/xml";

        public string Serialize(Document document)
        {
            var xmlSerializer = new XmlSerializer(typeof(Document));
            using var stringWriter = new StringWriter();
            xmlSerializer.Serialize(stringWriter, document);
            return stringWriter.ToString();
        }

        public Document Deserialize(string content)
        {
            var xmlSerializer = new XmlSerializer(typeof(Document));
            using var stringReader = new StringReader(content);
            return (Document)xmlSerializer.Deserialize(stringReader);
        }
    }
}
