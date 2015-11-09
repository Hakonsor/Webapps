using System.ComponentModel.DataAnnotations;

namespace Nips.DAL.Entities
{
    public class OrderLiner
    {
        [Key]
        public int id { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public int orderid { get; set; }
        public virtual Ordere order { get; set; }
        public virtual Produkter produkt { get; set; }
    }
}
