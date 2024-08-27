using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Data;
using TaskManager.Api.Models;
using TaskManager.Api.DTOs;
using TaskManager.Api.Messages;
using AutoMapper;
using TaskManager.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using MassTransit;

namespace TaskManager.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMessageScheduler _messageScheduler;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ApplicationDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint, IMessageScheduler messageScheduler, ILogger<TasksController> logger)
        {
            _context = context;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _messageScheduler = messageScheduler;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDto>>> GetTasks(
            [FromQuery] string? sortBy = null,
            [FromQuery] string? filterBy = null,
            [FromQuery] string? filterValue = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool sortDescending = false)
        {
            _logger.LogInformation("Retrieving tasks with filters: {FilterBy}={FilterValue}, sortBy={SortBy}, pageNumber={PageNumber}, pageSize={PageSize}, sortDescending={SortDescending}", filterBy, filterValue, sortBy, pageNumber, pageSize, sortDescending);
            var query = _context.TaskItems.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(filterBy) && !string.IsNullOrEmpty(filterValue))
            {
                query = filterBy.ToLower() switch
                {
                    "title" => query.Where(t => t.Title.Contains(filterValue)),
                    "description" => query.Where(t => t.Description.Contains(filterValue)),
                    "iscompleted" => query.Where(t => t.IsCompleted == bool.Parse(filterValue)),
                    _ => query
                };
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortBy.ToLower() switch
                {
                    "title" => sortDescending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
                    "duedate" => sortDescending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
                    "iscompleted" => sortDescending ? query.OrderByDescending(t => t.IsCompleted) : query.OrderBy(t => t.IsCompleted),
                    _ => query
                };
            }

            var tasks = await PaginatedList<Models.TaskItem>.CreateAsync(query, pageNumber, pageSize);
            var taskDtos = _mapper.Map<List<TaskItemDto>>(tasks);

            var response = new
            {
                Tasks = taskDtos,
                tasks.PageIndex,
                tasks.TotalPages,
                tasks.HasPreviousPage,
                tasks.HasNextPage
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItemDto>> GetTask(int id)
        {
            _logger.LogInformation("Retrieving task with ID: {TaskId}", id);
            var task = await _context.TaskItems.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return _mapper.Map<TaskItemDto>(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDto>> CreateTask(TaskItemDto taskDto)
        {
            var task = _mapper.Map<TaskItem>(taskDto);
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            var taskCreatedMessage = new TaskItemCreatedMessage
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CreatedAt = task.CreatedAt
            };

            await _publishEndpoint.Publish(taskCreatedMessage);

                // Schedule a reminder for 1 day before the due date
 
            await _messageScheduler.SchedulePublish(
                    task.DueDate.AddDays(-1),
                    new TaskItemReminderMessage
                    {
                        TaskId = task.Id,
                        TaskTitle = task.Title,
                        DueDate = task.DueDate
                    });

            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, _mapper.Map<TaskItemDto>(task));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskItemDto taskDto)
        {
            _logger.LogInformation("Updating task with ID: {TaskId}, New data: {@TaskDto}", id, taskDto);
            var task = await _context.TaskItems.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _mapper.Map(taskDto, task);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TaskExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _logger.LogInformation("Deleting task with ID: {TaskId}", id);
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}