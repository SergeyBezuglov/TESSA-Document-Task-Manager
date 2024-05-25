using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Контроллер для управления документами.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        /// <summary>
        /// Конструктор контроллера документов.
        /// </summary>
        /// <param name="documentRepository">Репозиторий документов.</param>
        public DocumentsController(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Получает список всех документов.
        /// </summary>
        /// <returns>Список документов.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            var documents = await _documentRepository.GetAllDocumentsAsync();
            return Ok(documents);
        }

        /// <summary>
        /// Получает документ по ID.
        /// </summary>
        /// <param name="id">ID документа.</param>
        /// <returns>Документ.</returns>
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

        /// <summary>
        /// Создает новый документ.
        /// </summary>
        /// <param name="document">Документ для создания.</param>
        /// <returns>Созданный документ.</returns>
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

        /// <summary>
        /// Обновляет существующий документ.
        /// </summary>
        /// <param name="id">ID документа для обновления.</param>
        /// <param name="document">Обновленный документ.</param>
        /// <returns>Результат операции.</returns>
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

        /// <summary>
        /// Удаляет документ по ID.
        /// </summary>
        /// <param name="id">ID документа для удаления.</param>
        /// <returns>Результат операции.</returns>
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
