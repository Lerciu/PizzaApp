using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizzaapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Pizzaapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracownicyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PracownicyController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pracownicy
        [HttpGet("ids")]
        public async Task<ActionResult<IEnumerable<int>>> GetPracownikIds()
        {
            var ids = await _context.Pracownicy.Select(p => p.id_pracownik).ToListAsync();
            return Ok(ids);
        }

        // GET: api/Pracownicy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pracownik>> GetPracownik(int id)
        {
            var pracownik = await _context.Pracownicy.FindAsync(id);

            if (pracownik == null)
            {
                return NotFound();
            }

            return pracownik;
        }

        // PUT: api/Pracownicy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePracownik(int id, Pracownik pracownik)
        {
            if (id != pracownik.id_pracownik)
            {
                return BadRequest();
            }

            _context.Entry(pracownik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PracownikExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pracownicy
        [HttpPost]
        public async Task<ActionResult<Pracownik>> CreatePracownik(Pracownik pracownik)
        {
            _context.Pracownicy.Add(pracownik);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPracownik", new { id = pracownik.id_pracownik }, pracownik);
        }

        // DELETE: api/Pracownicy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePracownik(int id)
        {
            var pracownik = await _context.Pracownicy.FindAsync(id);
            if (pracownik == null)
            {
                return NotFound();
            }

            _context.Pracownicy.Remove(pracownik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PracownikExists(int id)
        {
            return _context.Pracownicy.Any(e => e.id_pracownik == id);
        }
    }
}
