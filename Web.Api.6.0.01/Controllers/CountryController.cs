using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly PhonebookContext _context;

        public CountryController(PhonebookContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(long id)
        {
            var todoItem = await _context.Countries.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCountry(long id, Country country)
        {
            if (id != country.ID)
            {
                return BadRequest();
            }

            var foundcountry = await _context.Countries.FindAsync(id);
            if (foundcountry == null)
            {
                return NotFound();
            }

            foundcountry.CountryName = country.CountryName;
            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CountryExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPost]
        public async Task<ActionResult<Country>> CreateCountry(Country country)
        {
            var newcountry = new Country
            {
                
                CountryName = country.CountryName
            };

            _context.Countries.Add(newcountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCountry),
                new { id = newcountry.ID },
                ItemToDTO(newcountry));
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(long id)
        {
            var oldcountry = await _context.Countries.FindAsync(id);

            if (oldcountry == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(oldcountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(long id)
        {
            return _context.Countries.Any(country => country.ID == id);
        }

        private static Country ItemToDTO(Country country) =>
            new Country
            {
                ID = country.ID,
                CountryName = country.CountryName,
                CallNumber = country.CallNumber
            };
    }
}
