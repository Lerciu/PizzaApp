using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaapp.Models
{
    public class Skladniki
    {
        [Key]
        public int id_skladniki { get; set; }
        [Required]
        [StringLength(255)]
        public string nazwa { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal cena { get; set; }

        public virtual ICollection<PizzaSkladniki> PizzaSkladnikis { get; set; }
        public virtual ICollection<ZamowienieSkladniki> ZamowienieSkladnikis { get; set; }
    }

}
