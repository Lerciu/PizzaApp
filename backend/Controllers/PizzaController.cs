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
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PizzaController(AppDbContext context)
        {
            _context = context;
        }
        public class PizzaDTO
        {
            public int Id { get; set; }
            public string Nazwa { get; set; }
            public string Opis { get; set; }
            public decimal Cena { get; set; }
            // Include other properties as needed
        }

        // GET: api/Pizza
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaDTO>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas
                .Select(p => new PizzaDTO
                {
                    Id = p.id_pizza,
                    Nazwa = p.nazwa,
                    Opis = p.opis,
                    Cena = p.cena
                    // Map other properties as needed
                })
                .ToListAsync();

            return Ok(pizzas);
        }

        // GET: api/Pizza/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(int id)
        {
            var pizza = await _context.Pizzas
                .Include(p => p.PizzaSkladnikis)
                .ThenInclude(ps => ps.Skladniki)
                .FirstOrDefaultAsync(p => p.id_pizza == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }

        // PUT: api/Pizza/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePizza(int id, Pizza pizza)
        {
            if (id != pizza.id_pizza)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
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

        // POST: api/Pizza
        [HttpPost]
        public async Task<ActionResult<Pizza>> CreatePizza(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizza), new { id = pizza.id_pizza }, pizza);
        }

        // DELETE: api/Pizza/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(int id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(int id)
        {
            return _context.Pizzas.Any(e => e.id_pizza == id);
        }
    }
}
