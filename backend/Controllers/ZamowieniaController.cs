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
    public class ZamowieniaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ZamowieniaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Zamowienia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zamowienie>>> GetZamowienia()
        {
            return await _context.Zamowienia
         .Include(z => z.Klient)
         .Include(z => z.Pracownik) // Tylko jedno włączenie dla Pracownik
         .Include(z => z.PizzaZamowienie)
             .ThenInclude(pz => pz.Pizza)
         .Include(z => z.ZamowienieSkladniki)
             .ThenInclude(zs => zs.Skladniki)
         .ToListAsync();
        }

        // GET: api/Zamowienia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Zamowienie>> GetZamowienie(int id)
        {
            var zamowienie = await _context.Zamowienia
                .Include(z => z.Klient)
                
                .Include(z => z.Pracownik)
                .Include(z => z.PizzaZamowienie)
                .ThenInclude(pz => pz.Pizza)
                .Include(z => z.ZamowienieSkladniki)
                .ThenInclude(zs => zs.Skladniki)
                .FirstOrDefaultAsync(z => z.id_zamowienie == id);

            if (zamowienie == null)
            {
                return NotFound();
            }

            return zamowienie;
        }

        // PUT: api/Zamowienia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateZamowienie(int id, Zamowienie zamowienie)
        {
            if (id != zamowienie.id_zamowienie)
            {
                return BadRequest();
            }

            _context.Entry(zamowienie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZamowienieExists(id))
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

        // POST: api/Zamowienia
        [HttpPost]
        public async Task<ActionResult<Zamowienie>> CreateZamowienie([FromBody] ZamowienieDTO zamowienieDto)
        {
            Console.WriteLine($"Otrzymano zamówienie z ID pracownika: {zamowienieDto.id_pracownik}");
           

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Klient klient = await _context.Klienci
                                              .FirstOrDefaultAsync(k => k.email == zamowienieDto.Klient.email);
                if (klient == null)
                {
                    klient = new Klient
                    {
                        imie = zamowienieDto.Klient.imie,
                        nazwisko = zamowienieDto.Klient.nazwisko,
                        numertelefonu = zamowienieDto.Klient.numertelefonu,
                        email = zamowienieDto.Klient.email,
                        adres = zamowienieDto.Klient.adres,
                        numerdomu = zamowienieDto.Klient.numerdomu,
                        miasto = zamowienieDto.Klient.miasto
                    };
                    _context.Klienci.Add(klient);
                    await _context.SaveChangesAsync();
                }
                var pracownik = await _context.Pracownicy
                                       .FindAsync(zamowienieDto.id_pracownik);
                if (pracownik == null)
                {
                    return BadRequest("Podany identyfikator pracownika nie istnieje.");
                }

                var zamowienie = new Zamowienie
                {
                    data = DateTime.UtcNow,
                    id_klient = klient.id_klient,
                    id_pracownik = zamowienieDto.id_pracownik
                };
                _context.Zamowienia.Add(zamowienie);
                await _context.SaveChangesAsync();

                foreach (var pizzaZam in zamowienieDto.PizzaZamowienie)
                {
                    var pizzaZamowienie = new PizzaZamowienie
                    {
                        IdZamowienie = zamowienie.id_zamowienie,
                        IdPizza = pizzaZam.IdPizza,
                        ilosc = pizzaZam.ilosc
                    };
                    _context.PizzaZamowienia.Add(pizzaZamowienie);
                }

                await _context.SaveChangesAsync();
                transaction.Commit();

                return CreatedAtAction(nameof(GetZamowienie), new { id = zamowienie.id_zamowienie }, zamowienie);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Zamowienia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZamowienie(int id)
        {
            var zamowienie = await _context.Zamowienia.FindAsync(id);
            if (zamowienie == null)
            {
                return NotFound();
            }

            _context.Zamowienia.Remove(zamowienie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZamowienieExists(int id)
        {
            return _context.Zamowienia.Any(e => e.id_zamowienie == id);
        }
    }
}
