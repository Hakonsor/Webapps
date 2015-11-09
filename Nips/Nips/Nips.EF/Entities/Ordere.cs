using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nips.DAL.Entities
{
    public class Ordere
    {
        [Key]
        public int id { get; set; }
        public int kundeid { get; set; }
        public bool sendt { get; set; }     
        public virtual List<OrderLiner> orderline {get; set;}
        public virtual DateTime orderdato { get; set; }
        
    }
}
