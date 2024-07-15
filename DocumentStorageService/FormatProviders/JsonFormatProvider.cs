using Newtonsoft.Json;
using DocumentStorageService.Models;
using DocumentStorageService.Interfaces;
using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;

namespace DocumentStorageService.FormatProviders
{
    public class JsonFormatProvider : IFormatProvider
    {
        public string Format => "application/json";

        public string Serialize(Document document) => JsonConvert.SerializeObject(document);

        public Document Deserialize(string content) => JsonConvert.DeserializeObject<Document>(content);
    }
}
