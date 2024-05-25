using PIMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Persistence
{
    public interface IDocumentRepository
    {
        /// <summary>
        /// Получает все документы.
        /// </summary>
        /// <returns>Список всех документов.</returns>
        Task<IEnumerable<Document>> GetAllDocumentsAsync();

        /// <summary>
        /// Получает документ по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор документа.</param>
        /// <returns>Документ с указанным идентификатором.</returns>
        Task<Document> GetDocumentByIdAsync(string id);

        /// <summary>
        /// Добавляет новый документ.
        /// </summary>
        /// <param name="document">Документ для добавления.</param>
        /// <returns>Добавленный документ.</returns>
        Task<Document> AddDocumentAsync(Document document);

        // <summary>
        /// Обновляет существующий документ.
        /// </summary>
        /// <param name="document">Документ для обновления.</param>
        /// <returns>Обновленный документ.</returns>
        Task<Document> UpdateDocumentAsync(Document document);

        /// <summary>
        /// Удаляет документ по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор документа для удаления.</param>
        Task DeleteDocumentAsync(string id);
    }
}
