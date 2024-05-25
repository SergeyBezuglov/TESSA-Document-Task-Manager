using Microsoft.EntityFrameworkCore;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;
using PIMS.Infrastructure.Persistence.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Infrastructure.Persistence.Repositories.Common
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly PIMSDbContext _context;

        public DocumentRepository(PIMSDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получение всех документов с включением связанных задач
        /// </summary>
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.Include(d => d.Tasks).ToListAsync();
        }
        /// <summary>
        /// Получение документа по идентификатору с включением связанных задач
        /// </summary>
        public async Task<Document> GetDocumentByIdAsync(string id)
        {
            return await _context.Documents.Include(d => d.Tasks).FirstOrDefaultAsync(d => d.ID == id);
        }

        /// <summary>
        /// Добавление нового документа с проверкой и обновлением задач
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> AddDocumentAsync(Document document)
        {
            // Очистка трекера изменений контекста перед добавлением нового документа
            _context.ChangeTracker.Clear();  

            if (document.Tasks != null)
            {
                // Обработка задач, связанных с документом
                foreach (var task in document.Tasks)
                {
                    // Если задача уже существует, устанавливаем её состояние как измененное
                    if (_context.ProjectTasks.Any(t => t.ID == task.ID))
                    {
                        _context.Entry(task).State = EntityState.Modified;  
                    }
                    else
                    {
                        // Если задача новая, добавляем её в контекст
                        _context.ProjectTasks.Add(task);  
                    }
                }
            }

            // Добавление документа в контекст
            _context.Documents.Add(document);
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();
            return document;
        }

        /// <summary>
        /// Обновление существующего документа
        /// </summary>
        public async Task<Document> UpdateDocumentAsync(Document document)
        {
            // Устанавливаем состояние документа как измененное
            _context.Entry(document).State = EntityState.Modified;
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();
            return document;
        }
        /// <summary>
        /// Удаление документа по идентификатору
        /// </summary>
        public async Task DeleteDocumentAsync(string id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                // Удаление документа из контекста
                _context.Documents.Remove(document);
                // Сохранение изменений в базе данных
                await _context.SaveChangesAsync();
            }
        }
    }
}