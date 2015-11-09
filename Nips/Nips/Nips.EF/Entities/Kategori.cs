using System;
using System.ComponentModel.DataAnnotations;

namespace Nips.DAL.Entities
{
    public class Kategori
    {
        [Key]
        public int Id { get; set; }
        public String Navn { get; set; }
    }
}
