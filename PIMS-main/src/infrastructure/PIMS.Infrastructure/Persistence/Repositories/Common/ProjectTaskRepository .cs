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
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly PIMSDbContext _context;

        public ProjectTaskRepository(PIMSDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение всех задач
        /// </summary>
        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        public async Task<ProjectTask> GetTaskByIdAsync(string id)
        {
            return await _context.ProjectTasks.FindAsync(id);
        }

        /// <summary>
        ///  Добавление новой задачи
        /// </summary>
        public async Task<ProjectTask> AddTaskAsync(ProjectTask task)
        {
            // Проверка на наличие задачи с таким же ID
            if (_context.ProjectTasks.Any(t => t.ID == task.ID))
            {
                throw new InvalidOperationException("Task with the same ID already exists.");
            }

            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        /// <summary>
        /// Обновление существующей задачи
        /// </summary>
        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
        {
            var existingTask = await _context.ProjectTasks.FindAsync(task.ID);
            if (existingTask == null)
            {
                throw new InvalidOperationException("Task not found.");
            }

            _context.Entry(existingTask).CurrentValues.SetValues(task);
            await _context.SaveChangesAsync();
            return existingTask;
        }

        /// <summary>
        /// Удаление задачи по идентификатору
        /// </summary>
        public async Task DeleteTaskAsync(string id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                try
                {
                    // Находим все задачи, которые ссылаются на эту задачу
                    var dependentTasks = await _context.ProjectTasks
                        .Where(t => t.PreviousTaskID == id)
                        .ToListAsync();

                    // Удаляем все зависимые задачи
                    foreach (var dependentTask in dependentTasks)
                    {
                        _context.ProjectTasks.Remove(dependentTask);
                    }

                    // Удаляем саму задачу
                    _context.ProjectTasks.Remove(task);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    var innerExceptionMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                    throw new InvalidOperationException("An error occurred while deleting the task: " + ex.Message + " - Inner Exception: " + innerExceptionMessage, ex);
                }
            }
            else
            {
                throw new InvalidOperationException("Task not found.");
            }
        }
    }
}