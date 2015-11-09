using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nips.DAL.Entities
{
    public class Kunder
    {
        [Key]
        public int Id { get; set; }
        public String Fornavn { get; set; }
        public String Etternavn { get; set; }
        public String Email { get; set; }
        public String Tlf { get; set; }
        public String Adresse { get; set; }
        public int PoststedId { get; set; }
        public bool Admin { get; set; }
        public virtual Poststeder Poststeder { get; set; }
        public byte[] Passord { get; set; }
        public virtual List<Ordere> Order { get; set; }
    }
}
