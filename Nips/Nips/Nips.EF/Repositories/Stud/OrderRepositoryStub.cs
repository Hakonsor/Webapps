using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nips.Model.Entities;

namespace Nips.DAL.Repositories.Stud
{
    public class OrderRepositoryStub : DAL.Repositories.Stud.IOrderRepository
    {
        public int checkout(Handlevogn vare)
        {
            if (vare.userID == 0)
            {
                return -1;
            }
            return 1;
        }

        public List<Order> getOrder()
        {
            var orderliste = new List<Order>();
            var order = new Order()
            {
                id = 1,
                orderdato = new DateTime(2014, 1, 2),
                kundeid = 3,
                sendt = true,

            };
            orderliste.Add(order);
            orderliste.Add(order);
            orderliste.Add(order);
            return orderliste;

        }

        public List<OrderLine> getOrderLine(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> getOrders(int id)
        {
            throw new NotImplementedException();
        }

        public bool setSendt(int id)
        {
            throw new NotImplementedException();
        }
        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
