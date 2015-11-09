using BLL;
using Nips.BLL;
using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nips.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        private IOrderLogikk _OrdereBLL;

        public OrderController()
        {
            _OrdereBLL = new OrdereBLL();
        }

        public OrderController(IOrderLogikk stub)
        {
            _OrdereBLL = stub;
        }
        public bool GodkjentBruker()
        {
            Kunde k = (Kunde)Session["loggedInUser"];
            if (k == null || k.admin == false)
            {
                return false;
            }
            return true;
        }
        //hvis sessione er en admin så sendes dem til login
        public ActionResult Admin()
        {
            if (!GodkjentBruker()) return RedirectToAction("Login", "Kunde");

            var ordere = _OrdereBLL.getOrder();
            return View(ordere);
        }

        [HttpPost]
        public ActionResult Admin(int id)
        {
            
            if (_OrdereBLL.setSendt(id)) // markerer som sendt
            {   
                // noe
                return View();
            }
            else
            {
                return View();
            }
        }

        public ActionResult viewOrderhistorikk()
        {
            var user = (Kunde)Session["LoggedInUser"];
            if (user == null)
                return RedirectToAction("Front", "Main");

            var list = new List<Order>();
            var liste = _OrdereBLL.getOrder().Where(order => order.kundeid == user.kundeid).ToList();

            return View(liste);
        }

        public ActionResult visOrder(Order order)
        {
            var repo = new OrdereBLL();
            var orderlines = repo.getOrderLine(order.id);
            return View(orderlines);
        }

    }
}