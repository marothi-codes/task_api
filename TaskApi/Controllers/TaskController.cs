using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TaskApi.Models;

namespace TaskApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly TaskDbContext _context;

		public TaskController(TaskDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
		{
			return await _context.Tasks.AsNoTracking().ToArrayAsync();
		}

		[HttpPost]
		public async Task<ActionResult> PostTask(TaskModel task)
		{
			_context.Tasks.Add(task);
			await _context.SaveChangesAsync();
			return StatusCode(201);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateTask(int id, TaskModel task)
		{
			if (id != task.Id) return BadRequest();

			_context.Entry(task).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.Tasks.Any(x => x.Id == id))
					return NotFound();
				else throw;
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<TaskModel>> DeleteTask(int id)
		{
			var task = await _context.Tasks.FindAsync(id);

			if (task == null)
				return NotFound();

			_context.Tasks.Remove(task);
			await _context.SaveChangesAsync();

			return task;
		}
	}
}
