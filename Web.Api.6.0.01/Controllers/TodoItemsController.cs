using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly PhonebookContext _context;

        public TodoItemsController(PhonebookContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhoneBook>>> GetTodoItems()
        {
            return await _context.TodoItems
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneBook>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, PhoneBook PhoneBook)
        {
            if (id != PhoneBook.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = PhoneBook.Name;
            todoItem.IsComplete = PhoneBook.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<PhoneBook>> CreateTodoItem(PhoneBook PhoneBook)
        {
            var todoItem = new PhoneBook
            {
                IsComplete = PhoneBook.IsComplete,
                Name = PhoneBook.Name
            };

            _context.TodoItems.Add(PhoneBook);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static PhoneBook ItemToDTO(PhoneBook PhoneBook) =>
            new PhoneBook
            {
                Id = PhoneBook.Id,
                Name = PhoneBook.Name,
                IsComplete = PhoneBook.IsComplete
            };
    }
}
