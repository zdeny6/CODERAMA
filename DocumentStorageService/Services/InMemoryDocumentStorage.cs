using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentStorageService.Models;
using DocumentStorageService.Interfaces;

namespace DocumentStorageService.Services
{
    public class InMemoryDocumentStorage : IDocumentStorage
    {
        private readonly Dictionary<string, Document> _storage = new();

        public Task SaveAsync(Document document)
        {
            _storage[document.Id] = document;
            return Task.CompletedTask;
        }

        public Task<Document> GetAsync(string id)
        {
            _storage.TryGetValue(id, out var document);
            return Task.FromResult(document);
        }

        public Task UpdateAsync(Document document)
        {
            _storage[document.Id] = document;
            return Task.CompletedTask;
        }
    }
}
