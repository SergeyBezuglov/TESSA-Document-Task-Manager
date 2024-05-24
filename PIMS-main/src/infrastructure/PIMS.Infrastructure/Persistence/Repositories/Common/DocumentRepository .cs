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
            _context.Documents.Add(document);

            foreach (var task in document.Tasks)
            {
                var existingTask = await _context.ProjectTasks.FindAsync(task.ID);
                if (existingTask != null)
                {
                    // Если задача уже существует в базе данных, используем ее
                    task.DocumentID = document.ID;
                    _context.Entry(task).State = EntityState.Modified; // Изменяем состояние на Modified, чтобы обновить существующую задачу
                }
                else
                {
                    // Если задачи нет в базе данных, добавляем ее
                    task.DocumentID = document.ID;
                    _context.Entry(task).State = EntityState.Added;
                }
            }

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