using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Pizzaapp.Models
{
    public class Pizza
    {
        [Key]
        public int id_pizza { get; set; }
        [Required]
        [StringLength(255)]
        public string nazwa { get; set; }
        public string opis { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal cena { get; set; }
        [JsonIgnore]
        public virtual ICollection<PizzaSkladniki> PizzaSkladnikis { get; set; }
        [JsonIgnore]
        public virtual ICollection<PizzaZamowienie> PizzaZamowienies { get; set; }
    }
}


