using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using DocumentStorageService.Controllers;
using DocumentStorageService.Interfaces;
using DocumentStorageService.Models;
using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;
using DocumentStorageService.FormatProviders;

namespace DocumentStorageService.Tests.Controllers
{
    public class DocumentsControllerTests
    {
        [Fact]
        public async Task Post_Should_Return_CreatedAtActionResult()
        {
            // Priprava
            var mockStorage = new Mock<IDocumentStorage>();
            var mockFormatProviders = new Mock<IEnumerable<IFormatProvider>>();
            var controller = new DocumentsController(mockStorage.Object, mockFormatProviders.Object);
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");

            // POST
            var result = await controller.Post(document);

            // Overeni
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.Get), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task Get_Should_Return_Document_When_Exists()
        {
            // Priprava
            var mockStorage = new Mock<IDocumentStorage>();
            var mockFormatProviders = new Mock<IEnumerable<IFormatProvider>>();
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");

            mockStorage.Setup(s => s.GetAsync("some-unique-identifier1")).ReturnsAsync(document);
            mockFormatProviders.Setup(fp => fp.GetEnumerator()).Returns(new List<IFormatProvider> { new JsonFormatProvider() }.GetEnumerator());

            var controller = new DocumentsController(mockStorage.Object, mockFormatProviders.Object);

            // GET JSON
            var result = await controller.Get("some-unique-identifier1", "application/json");

            // Overeni
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("application/json", contentResult.ContentType);
        }

        [Fact]
        public async Task Get_Should_Return_NotFound_When_Document_Does_Not_Exist()
        {
            // Priprava
            var mockStorage = new Mock<IDocumentStorage>();
            var mockFormatProviders = new Mock<IEnumerable<IFormatProvider>>();

            mockStorage.Setup(s => s.GetAsync("1")).ReturnsAsync((Document)null);

            var controller = new DocumentsController(mockStorage.Object, mockFormatProviders.Object);

            // GET
            var result = await controller.Get("1", "application/json");

            // OVERENI
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Put_Should_Return_NoContent_When_Document_Updated()
        {
            // Priprava
            var mockStorage = new Mock<IDocumentStorage>();
            var mockFormatProviders = new Mock<IEnumerable<IFormatProvider>>();
            var document = new Document { Id = "some-unique-identifier1", Tags = new List<string> { "important", ".net", "test" }, Data = new Dictionary<string, object>() };
            document.Data.Add("some", "data");
            document.Data.Add("optional", "fields");

            var controller = new DocumentsController(mockStorage.Object, mockFormatProviders.Object);

            // PUT
            var result = await controller.Put("some-unique-identifier1", document);

            // Overeni
            Assert.IsType<NoContentResult>(result);
        }
    }
}
