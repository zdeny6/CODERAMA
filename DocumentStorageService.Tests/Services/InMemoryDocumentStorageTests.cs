using Xunit;
using DocumentStorageService.Services;
using DocumentStorageService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentStorageService.Tests.Services
{
    public class InMemoryDocumentStorageTests
    {
        [Fact]
        public async Task SaveAsync_Should_Store_Document()
        {
            // Priprava
            var storage = new InMemoryDocumentStorage();
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");

            // Ulozeni
            await storage.SaveAsync(document);
            var storedDocument = await storage.GetAsync("some-unique-identifier1");

            // Overeni
            Assert.NotNull(storedDocument);
            Assert.Equal(document.Id, storedDocument.Id);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Stored_Document()
        {
            // Priprava
            var storage = new InMemoryDocumentStorage();
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");
            await storage.SaveAsync(document);

            // Cteni
            var storedDocument = await storage.GetAsync("some-unique-identifier1");

            // Overeni
            Assert.NotNull(storedDocument);
            Assert.Equal(document.Id, storedDocument.Id);
        }

        [Fact]
        public async Task GetAsync_Should_Return_Null_When_Document_Does_Not_Exist()
        {
            // Priprava
            var storage = new InMemoryDocumentStorage();

            // Cteni
            var storedDocument = await storage.GetAsync("some-unique-identifier1");

            // Overeni
            Assert.Null(storedDocument);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Stored_Document()
        {
            // Priprava
            var storage = new InMemoryDocumentStorage();
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");
            await storage.SaveAsync(document);

            var updatedDocument = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test", "next test" }, Data = new Dictionary<string, object>() };
            updatedDocument.Data.Add("some", "data");
            updatedDocument.Data.Add("optional", "fields");
            updatedDocument.Data.Add("test", "test");

            // UPDATE
            await storage.UpdateAsync(updatedDocument);
            var storedDocument = await storage.GetAsync("some-unique-identifier1");

            // OVERENI
            Assert.NotNull(storedDocument);
            Assert.Equal(updatedDocument.Tags, storedDocument.Tags);
        }
    }
}
