using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nips.DAL.Repositories.Stud
{
    public interface IOrderRepository
    {

        int checkout(Handlevogn vare);
        List<Order> getOrder();
        bool setSendt(int id);
        List<Order> getOrders(int id);
        List<OrderLine> getOrderLine(int id);
        void writeToFile(Exception e);

    }
}

