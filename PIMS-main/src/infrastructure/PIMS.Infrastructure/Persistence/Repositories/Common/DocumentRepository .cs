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

        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _context.Documents.Include(d => d.Tasks).ToListAsync();
        }

        public async Task<Document> GetDocumentByIdAsync(string id)
        {
            return await _context.Documents.Include(d => d.Tasks).FirstOrDefaultAsync(d => d.ID == id);
        }

        public async Task<Document> AddDocumentAsync(Document document)
        {
            _context.ChangeTracker.Clear();  // Очистка контекста перед добавлением нового документа

            if (document.Tasks != null)
            {
                foreach (var task in document.Tasks)
                {
                    if (_context.ProjectTasks.Any(t => t.ID == task.ID))
                    {
                        _context.Entry(task).State = EntityState.Modified;  // Обновление существующей задачи
                    }
                    else
                    {
                        _context.ProjectTasks.Add(task);  // Добавление новой задачи
                    }
                }
            }

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task<Document> UpdateDocumentAsync(Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return document;
        }

        public async Task DeleteDocumentAsync(string id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document != null)
            {
                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();
            }
        }
    }
}