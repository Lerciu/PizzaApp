using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzaapp.Models
{
    public class Zamowienie
    {
        [Key]
        public int id_zamowienie { get; set; }
        [Required]
        public DateTime data { get; set; }

        [ForeignKey("Klient")]
        public int id_klient { get; set; }
        public virtual Klient Klient { get; set; }

        [ForeignKey("Pracownik")]
        public int id_pracownik { get; set; }
        public virtual Pracownik Pracownik { get; set; }

        public virtual ICollection<PizzaZamowienie> PizzaZamowienie { get; set; }
        public virtual ICollection<ZamowienieSkladniki> ZamowienieSkladniki { get; set; }
    }
}
