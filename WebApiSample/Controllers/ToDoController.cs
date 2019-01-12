using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSample.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiSample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController : ControllerBase
	{
		private readonly ToDoContext _context;

		public TodoController(ToDoContext context)
		{
			_context = context;

			if (_context.TodoItems.Count() == 0)
			{
				// Create a new TodoItem if collection is empty,
				// which means you can't delete all TodoItems.
				_context.TodoItems.Add(new ToDoItem { Name = "Item1" });
				_context.SaveChanges();
			}
		}

		// GET: api/Todo
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ToDoItem>>> GetTodoItems()
		{
			return await _context.TodoItems.ToListAsync();
		}

		// GET: api/Todo/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ToDoItem>> GetTodoItem(long id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);

			if (todoItem == null)
			{
				return NotFound();
			}

			return todoItem;
		}

		// POST: api/Todo
		[HttpPost]
		public async Task<ActionResult<ToDoItem>> PostTodoItem(ToDoItem todoItem)
		{
			_context.TodoItems.Add(todoItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
		}

		// PUT: api/Todo/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutTodoItem(long id, ToDoItem todoItem)
		{
			if (id != todoItem.Id)
			{
				return BadRequest();
			}

			_context.Entry(todoItem).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/Todo/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<ToDoItem>> DeleteTodoItem(long id)
		{
			var todoItem = await _context.TodoItems.FindAsync(id);
			if (todoItem == null)
			{
				return NotFound();
			}

			_context.TodoItems.Remove(todoItem);
			await _context.SaveChangesAsync();

			return todoItem;
		}
	}
}
