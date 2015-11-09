using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nips.Model.Entities
{
    public class Order
    {
        // GET: Order
        [Key]
        [DisplayName("Ordrenummer")]
        public int id { get; set; }
        [DisplayName("Ordredato")]
        public DateTime orderdato { get; set; }
        [DisplayName("Kunde Id")]
        public int kundeid { get; set; }
        public List<OrderLine> orderline { get; set; }
        public Boolean sendt { get; set; } 
    }
}