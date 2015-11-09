using BLL;
using Nips.DAL.Repositories;
using Nips.Model.Entities;
using System.Collections.Generic;
using System;
using Nips.DAL.Repositories.Stud;

namespace Nips.BLL
{
   public class OrdereBLL : IOrderLogikk
    {
        private IOrderRepository _repository;

        public OrdereBLL()
        {
            System.Diagnostics.Debug.WriteLine("riktig");
            _repository = new OrderRepository();
        }
        public OrdereBLL(IOrderRepository stub)
        {
            System.Diagnostics.Debug.WriteLine("kfake bllt");
            _repository = stub;
        }
        public int checkout(Handlevogn vogn)
        {
            return _repository.checkout(vogn);
        }
        public List<Order> getOrder()
        {
           return _repository.getOrder();
        }
        public bool setSendt(int id)
        {
            return _repository.setSendt(id);
        }

        public List<Order> getOrders(int id)
        {
            return _repository.getOrders(id);
        }
        public List<OrderLine> getOrderLine(int id)
        {
            return _repository.getOrderLine(id);
        }

        public void writeToFile(Exception e)
        {
            throw new NotImplementedException();
        }
    }
}
