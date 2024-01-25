using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaapp.Models
{
    public class PizzaZamowienie
    {
        [Key]
        [Column("id_pizza_zamowienie")]
        public int IdPizzaZamowienie { get; set; }
        [ForeignKey("Pizza")]
        [Column("id_pizza")]
        public int IdPizza { get; set; }
        public Pizza Pizza { get; set; }
        [Column("id_zamowienie")]
        public int IdZamowienie { get; set; }
        public Zamowienie Zamowienie { get; set; }

        public int ilosc { get; set; }

        public ICollection<ZamowienieSkladniki> ZamowienieSkladniki { get; set; }
    }
}
