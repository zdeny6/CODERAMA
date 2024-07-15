using MessagePack;
using DocumentStorageService.Models;
using DocumentStorageService.Interfaces;
using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;

namespace DocumentStorageService.FormatProviders
{
    public class MessagePackFormatProvider : IFormatProvider
    {
        public string Format => "application/x-msgpack";

        public string Serialize(Document document)
        {
            var bytes = MessagePackSerializer.Serialize(document);
            return MessagePackSerializer.ConvertToJson(bytes);
        }

        public Document Deserialize(string content)
        {
            var bytes = MessagePackSerializer.ConvertFromJson(content);
            return MessagePackSerializer.Deserialize<Document>(bytes);
        }
    }
}
