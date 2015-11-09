using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nips.DAL.Entities
{
    public class Spørsmålene
    {
        [Key]
        public int id { get; set; }
       
        public String email { get; set; }
       
        public String Beskrivelse { get; set; }
    }
}
