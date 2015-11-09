using System;
using System.ComponentModel.DataAnnotations;

namespace Nips.DAL.Entities
{
    public class Produkter
    {
        [Key]
        public int id { get; set; }
        public String Navn { get; set; }
        public String Beskrivelse { get; set; }
        public int Antall { get; set; }
        public String Kategori { get; set; }
        public int Pris { get; set; }
        public string ImageUrl { get; set; }
    }
}
