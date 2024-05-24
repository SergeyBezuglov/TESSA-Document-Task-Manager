using PIMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMS.Application.Common.Interfaces.Persistence
{
    public interface IProjectTaskRepository
    {
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask> GetTaskByIdAsync(string id);
        Task<ProjectTask> AddTaskAsync(ProjectTask task);
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task);
        Task DeleteTaskAsync(string id);
    }
}
