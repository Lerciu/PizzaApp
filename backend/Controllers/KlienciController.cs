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
    public class KlienciController : ControllerBase
    {
        private readonly AppDbContext _context;

        public KlienciController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Klienci
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Klient>>> GetKlienci()
        {
            return await _context.Klienci.ToListAsync();
        }

        // GET: api/Klienci/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Klient>> GetKlient(int id)
        {
            var klient = await _context.Klienci.FindAsync(id);

            if (klient == null)
            {
                return NotFound();
            }

            return klient;
        }

        // PUT: api/Klienci/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKlient(int id, Klient klient)
        {
            if (id != klient.id_klient)
            {
                return BadRequest();
            }

            _context.Entry(klient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KlientExists(id))
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

        // POST: api/Klienci
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Klient>> CreateKlient(Klient klient)
        {
            _context.Klienci.Add(klient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKlient", new { id = klient.id_klient }, klient);
        }

        // DELETE: api/Klienci/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKlient(int id)
        {
            var klient = await _context.Klienci.FindAsync(id);
            if (klient == null)
            {
                return NotFound();
            }

            _context.Klienci.Remove(klient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KlientExists(int id)
        {
            return _context.Klienci.Any(e => e.id_klient == id);
        }
    }
}
