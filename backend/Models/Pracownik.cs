using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Pizzaapp.Models
{
    public class Pracownik
    {
        [Key]
        public int id_pracownik { get; set; }
        [Required]
        [StringLength(50)]
        public string nazwa { get; set; }

        // Relacje
        [JsonIgnore]
        public virtual ICollection<Zamowienie> Zamowienia { get; set; }
    }
}
