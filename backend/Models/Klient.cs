using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaapp.Models
{
    public class Klient
    {
        [Key]
        public int id_klient { get; set; }
        [Required]
        [StringLength(50)]
        public string imie { get; set; }
        [Required]
        [StringLength(50)]
        public string nazwisko { get; set; }
        [Required]
        [StringLength(15)]
        public string numertelefonu { get; set; }
        [Required]
        [StringLength(255)]
        public string email { get; set; }
        [Required]
        [StringLength(255)]
        public string adres { get; set; }
        [Required]
        [StringLength(10)]
        public string numerdomu { get; set; }
        [Required]
        [StringLength(50)]
        public string miasto { get; set; }

        public virtual ICollection<Zamowienie> Zamowienies { get; set; }
    }

}
