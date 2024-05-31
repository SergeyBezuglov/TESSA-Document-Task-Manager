using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;

namespace PIMS.Web.Controllers.v1
{
    /// <summary>
    /// Контроллер для управления задачами проектов.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskRepository _taskRepository;

        /// <summary>
        /// Конструктор контроллера задач проектов.
        /// </summary>
        /// <param name="taskRepository">Репозиторий задач проектов.</param>
        public ProjectTasksController(IProjectTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Получает список всех задач проектов.
        /// </summary>
        /// <returns>Список задач проектов.</returns>
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            if (tasks == null || !tasks.Any())
            {
                return NoContent();
            }
            return Ok(tasks);
        }

        /// <summary>
        /// Получает задачу проекта по ID.
        /// </summary>
        /// <param name="id">ID задачи проекта.</param>
        /// <returns>Задача проекта.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound($"Task with ID {id} not found.");
            }

            return Ok(task);
        }

        /// <summary>
        /// Создает новую задачу проекта.
        /// </summary>
        /// <param name="task">Задача проекта для создания.</param>
        /// <returns>Созданная задача проекта.</returns>
        [HttpPost]
        public async Task<IActionResult> PostTask([FromBody] ProjectTask task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }

            try
            {
                await _taskRepository.AddTaskAsync(task);
                return CreatedAtAction(nameof(GetTask), new { id = task.ID }, task);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        /// <summary>
        /// Обновляет существующую задачу проекта.
        /// </summary>
        /// <param name="id">ID задачи проекта для обновления.</param>
        /// <param name="task">Обновленная задача проекта.</param>
        /// <returns>Результат операции.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, [FromBody] ProjectTask task)
        {
            if (task == null || id != task.ID)
            {
                return BadRequest("Task ID mismatch.");
            }

            try
            {
                await _taskRepository.UpdateTaskAsync(task);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound($"Task with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        /// <summary>
        /// Удаляет задачу проекта по ID.
        /// </summary>
        /// <param name="id">ID задачи проекта для удаления.</param>
        /// <returns>Результат операции.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            try
            {
                await _taskRepository.DeleteTaskAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }
    }
}