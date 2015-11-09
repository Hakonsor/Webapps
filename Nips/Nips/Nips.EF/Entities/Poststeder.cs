using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nips.DAL.Entities
{
    public class Poststeder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PoststederID { get; set; }
        public String Poststed { get; set; }
    }
    
}
