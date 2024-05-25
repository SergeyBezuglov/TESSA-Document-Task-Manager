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
        /// <summary>
        /// Получает все задачи.
        /// </summary>
        /// <returns>Список всех задач.</returns>
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();

        /// <summary>
        /// Получает задачу по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи.</param>
        /// <returns>Задача с указанным идентификатором.</returns>
        Task<ProjectTask> GetTaskByIdAsync(string id);

        /// <summary>
        /// Добавляет новую задачу.
        /// </summary>
        /// <param name="task">Задача для добавления.</param>
        /// <returns>Добавленная задача.</returns>
        Task<ProjectTask> AddTaskAsync(ProjectTask task);

        /// <summary>
        /// Обновляет существующую задачу.
        /// </summary>
        /// <param name="task">Задача для обновления.</param>
        /// <returns>Обновленная задача.</returns>
        Task<ProjectTask> UpdateTaskAsync(ProjectTask task);

        /// <summary>
        /// Удаляет задачу по ее идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор задачи для удаления.</param>
        Task DeleteTaskAsync(string id);
    }
}
