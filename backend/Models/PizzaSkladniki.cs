using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaapp.Models
{
    public class PizzaSkladniki
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("pizza")]
        public int id_pizza { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("skladniki")]
        public int id_skladniki { get; set; }
        public int ilosc { get; set; }

        public virtual Pizza Pizza { get; set; }
        public virtual Skladniki Skladniki { get; set; }
    }
}
