using DocumentStorageService.Models;

namespace DocumentStorageService.Interfaces
{
    public interface IFormatProvider
    {
        string Format { get; }
        string Serialize(Document document);
        Document Deserialize(string content);
    }
}
