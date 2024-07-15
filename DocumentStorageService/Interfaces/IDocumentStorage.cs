using DocumentStorageService.Models;

namespace DocumentStorageService.Interfaces
{
    public interface IDocumentStorage
    {
        Task SaveAsync(Document document);
        Task<Document> GetAsync(string id);
        Task UpdateAsync(Document document);
    }
}
