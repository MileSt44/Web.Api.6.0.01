using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBookApi.Models;
using Web.Api._6._0._01.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ISenderService _service;
        private readonly PhonebookContext _context;
        public CityController(ISenderService service, PhonebookContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            _service.SendEmail("Probajemo pizdarije radit");
            return await _context.Cities
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(long id)
        {
            var todoItem = await _context.Cities.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(long id, City city)
        {
            if (id != city.ID)
            {
                return BadRequest();
            }

            var foundcity = await _context.Cities.FindAsync(id);
            if (foundcity == null)
            {
                return NotFound();
            }

            foundcity.CityName = city.CityName;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!CityExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<City>> CreateCity(City city)
        {
            var newcity = new City
            {

                CityName = city.CityName
            };

            _context.Cities.Add(newcity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetCity),
                new { id = newcity.ID },
                ItemToDTO(newcity));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(long id)
        {
            var oldcity = await _context.Cities.FindAsync(id);

            if (oldcity == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(oldcity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(long id)
        {
            return _context.Cities.Any(city => city.ID == id);
        }

        private static City ItemToDTO(City city) =>
            new City
            {
                ID = city.ID,

            };
    }
}
