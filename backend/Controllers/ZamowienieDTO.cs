using System;
using System.Collections.Generic;

namespace Pizzaapp.Models
{
    public class ZamowienieDTO
    {
        public DateTime data { get; set; }
        public KlientDTO Klient { get; set; }
        public int id_pracownik { get; set; }
        public List<PizzaZamowienieDTO> PizzaZamowienie { get; set; }
    //    public List<ZamowienieSkladnikiDTO> ZamowienieSkladniki { get; set; }
    }

    public class KlientDTO
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string numertelefonu { get; set; }
        public string email { get; set; }
        public string adres { get; set; }
        public string numerdomu { get; set; }
        public string miasto { get; set; }
    }

    public class PizzaZamowienieDTO
    {
        public int IdPizza { get; set; }
        public int ilosc { get; set; }
    }

    public class ZamowienieSkladnikiDTO
    {
        public int IdSkladniki { get; set; }
        public int ilosc { get; set; }
    }
}
