using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;

namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentsController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _documentRepository.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(string id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument([FromBody] Document document)
        {
            if (document == null)
            {
                return BadRequest("Document is null.");
            }

            try
            {
                await _documentRepository.AddDocumentAsync(document);
                return CreatedAtAction(nameof(GetDocument), new { id = document.ID }, document);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(string id, [FromBody] Document document)
        {
            if (document == null || id != document.ID)
            {
                return BadRequest("Document ID mismatch.");
            }

            await _documentRepository.UpdateDocumentAsync(document);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            var document = await _documentRepository.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            await _documentRepository.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}
