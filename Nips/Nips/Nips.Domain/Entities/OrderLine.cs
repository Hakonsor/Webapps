
namespace Nips.Model.Entities
{
    public class OrderLine
    {
        
        // GET: OrderLine
        public int id { get; set; }
        public int produktid { get; set; }
        public int antall { get; set; }
        public int orderid { get; set; }
        public Order order { get; set; }
        public Produkt produkt { get; set; }

    }
}