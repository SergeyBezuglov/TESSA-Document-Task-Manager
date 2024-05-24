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

        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        public async Task<ProjectTask> GetTaskByIdAsync(string id)
        {
            return await _context.ProjectTasks.FindAsync(id);
        }

        public async Task<ProjectTask> AddTaskAsync(ProjectTask task)
        {
            var existingTask = await _context.ProjectTasks.FindAsync(task.ID);
            if (existingTask == null)
            {
                _context.ProjectTasks.Add(task);
                await _context.SaveChangesAsync();
                return task;
            }
            else
            {
                throw new InvalidOperationException("Task with the same ID already exists.");
            }
        }

        public async Task<ProjectTask> UpdateTaskAsync(ProjectTask task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(string id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}