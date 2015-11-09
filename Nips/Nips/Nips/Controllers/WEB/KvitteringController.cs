using System.Web.Mvc;

namespace Nips.Controllers.WEB
{
    public class KvitteringController : Controller
    {
        // GET: Kvittering
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kvittering()
        {
            ViewBag.confirmation = true;
            return View("Kvittering");
        }
    }
}