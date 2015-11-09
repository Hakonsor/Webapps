using Nips.Content;
using Nips.Model.Entities;
using System.Web.Mvc;

namespace Nips.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            /*
            var kunde = (Kunde)Session["loggedInUser"];
            if (kunde != null && kunde.fornavn.Equals("Håkon"))
            {
                new Kundegenerator().generer();
                System.Diagnostics.Debug.WriteLine("Mohahaha");
            }
            */
            return View();
        }
    }
}