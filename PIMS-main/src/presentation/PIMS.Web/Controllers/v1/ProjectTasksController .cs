using Microsoft.AspNetCore.Mvc;
using PIMS.Application.Common.Interfaces.Persistence;
using PIMS.Domain;

namespace PIMS.Web.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTaskRepository _taskRepository;

        public ProjectTasksController(IProjectTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

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
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(string id, [FromBody] ProjectTask task)
        {
            if (task == null || id != task.ID)
            {
                return BadRequest("Task ID mismatch.");
            }

            await _taskRepository.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(string id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}