using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pizzaapp.Models
{
    public class ZamowienieSkladniki
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("zamowienie")]
        public int id_zamowienie { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("skladniki")]
        public int id_skladniki { get; set; }
        [ForeignKey("pizzazamowienie")]
        public int? id_pizza_zamowienie { get; set; }
        public int ilosc { get; set; }
        [JsonIgnore]
        public virtual Zamowienie Zamowienie { get; set; }
        [JsonIgnore]
        public virtual Skladniki Skladniki { get; set; }
        [JsonIgnore]
        public virtual PizzaZamowienie PizzaZamowienie { get; set; }
    }
}
