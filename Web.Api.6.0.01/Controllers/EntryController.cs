using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly PhonebookContext _context;

        public EntryController(PhonebookContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Entry>> GetEntry(long id)
        {
            var todoItem = await _context.Entries.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(long id, Entry entry)
        {
            if (id != entry.ID)
            {
                return BadRequest();
            }

            var foundentry = await _context.Entries.FindAsync(id);
            if (foundentry == null)
            {
                return NotFound();
            }

            foundentry.EntryName = entry.EntryName;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EntryExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<Entry>> CreateEntry(Entry entry)
        {
            var newentry = new Entry
            {
                
                EntryName = entry.EntryName
            };

            _context.Entries.Add(newentry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetEntry),
                new { id = newentry.ID },
                ItemToDTO(newentry));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(long id)
        {
            var oldentry = await _context.Entries.FindAsync(id);

            if (oldentry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(oldentry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntryExists(long id)
        {
            return _context.Entries.Any(entry => entry.ID == id);
        }

        private static Entry ItemToDTO(Entry entry) =>
            new Entry
            {
                ID = entry.ID,
             
            };
    }
}
