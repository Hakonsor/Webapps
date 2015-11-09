using System.Web.Mvc;

namespace Nips.Controllers
{
    public class BildeController : Controller
    {
        // GET: Bilde
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult visBilde()
        {
            return View();
        }

        public ActionResult Opplasting(int id)
        {
            ViewBag.VareId = id;
            return View();
        }

    }
}
