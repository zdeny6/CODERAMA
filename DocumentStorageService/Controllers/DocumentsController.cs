using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentStorageService.Models;
using DocumentStorageService.Interfaces;
using IFormatProvider = DocumentStorageService.Interfaces.IFormatProvider;

namespace DocumentStorageService.Controllers
{
    [ApiController]
    [Route("documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentStorage _storage;
        private readonly IEnumerable<IFormatProvider> _formatProviders;

        public DocumentsController(IDocumentStorage storage, IEnumerable<IFormatProvider> formatProviders)
        {
            _storage = storage;
            _formatProviders = formatProviders;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Document document)
        {
            await _storage.SaveAsync(document);
            return CreatedAtAction(nameof(Get), new { id = document.Id }, document);
        }

        [HttpGet("{id}")]
        //Use [FromQuery] due to SWAGGER else is better to use [FromHeader(Name = "Accept")]
        public async Task<IActionResult> Get(string id, [FromQuery(Name = "Accept")] string accept)
        {
            var document = await _storage.GetAsync(id);
            if (document == null)
                return NotFound();

            var formatProvider = _formatProviders.FirstOrDefault(p => p.Format == accept);
            if (formatProvider == null)
                return BadRequest("Unsupported format");

            var content = formatProvider.Serialize(document);
            return Content(content, formatProvider.Format);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Document document)
        {
            if (id != document.Id)
                return BadRequest();

            await _storage.UpdateAsync(document);
            return NoContent();
        }
    }
}
