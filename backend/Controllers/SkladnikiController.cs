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
    public class SkladnikiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SkladnikiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Skladniki
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skladniki>>> GetSkladnikis()
        {
            return await _context.Skladnikis.ToListAsync();
        }

        // GET: api/Skladniki/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Skladniki>> GetSkladniki(int id)
        {
            var skladniki = await _context.Skladnikis.FindAsync(id);

            if (skladniki == null)
            {
                return NotFound();
            }

            return skladniki;
        }

        // PUT: api/Skladniki/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkladniki(int id, Skladniki skladniki)
        {
            if (id != skladniki.id_skladniki)
            {
                return BadRequest();
            }

            _context.Entry(skladniki).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkladnikiExists(id))
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

        // POST: api/Skladniki
        [HttpPost]
        public async Task<ActionResult<Skladniki>> CreateSkladniki(Skladniki skladniki)
        {
            _context.Skladnikis.Add(skladniki);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkladniki), new { id = skladniki.id_skladniki }, skladniki);
        }

        // DELETE: api/Skladniki/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkladniki(int id)
        {
            var skladniki = await _context.Skladnikis.FindAsync(id);
            if (skladniki == null)
            {
                return NotFound();
            }

            _context.Skladnikis.Remove(skladniki);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SkladnikiExists(int id)
        {
            return _context.Skladnikis.Any(e => e.id_skladniki == id);
        }
    }
}
