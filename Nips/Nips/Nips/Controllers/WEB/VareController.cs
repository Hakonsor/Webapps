using BLL.Test;
using Nips.BLL;
using Nips.Model.Entities;
using System.Web.Mvc;


namespace Nips.Controllers
{
    public class VareController : Controller
    {
        private IProduktLogikk _produktBLL;

        public VareController()
        {
            _produktBLL = new ProduktBLL();
        }

        public VareController(IProduktLogikk stub)
        {
            _produktBLL = stub;
        }

        public ActionResult Liste()
        {
            if (!GodkjentBruker())
            {
                return RedirectToAction("LogIn", "Kunde");
            }
            var produktliste = _produktBLL.alleVare();
            return View(produktliste);
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

        public ActionResult Details(int id)
        {
            var result = _produktBLL.hentEnVare(id);
            if (result == null) return RedirectToAction("Liste");
            return View(result);

        }


        public ActionResult Registrer()
        {
            if (!GodkjentBruker()) return RedirectToAction("LogIn", "Kunde");
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Registrer(Produkt nyVare)
        {
            if (ModelState.IsValid)
            {

                int vareLagret = _produktBLL.lagreVare(nyVare);
                if (vareLagret == (-1))
                {
                    ViewBag.VareFeil = false;
                    return View();
                }
                return RedirectToAction("Liste");

            }
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Endre(int id, Produkt endreVare)
        {
            if (ModelState.IsValid)
            {
                _produktBLL.endreVare(id, endreVare);
                var produkt = _produktBLL.hentEnVare(id);
                if (produkt == null)
                {
                    ViewBag.feil = true;
                    return View();
                }
                return View(produkt);
            }
            return View();
        }
        public ActionResult Endre(int id)
        {
            if (!GodkjentBruker()) return RedirectToAction("LogIn", "Kunde");
            
            var produkt = _produktBLL.hentEnVare(id);
            if(produkt == null) return RedirectToAction("Liste");
            return View(produkt);

        }

        public ActionResult Slett(int id)
        {
            if (!GodkjentBruker()) return RedirectToAction("LogIn", "Kunde");
            
            var produkt = _produktBLL.hentEnVare(id);
            if (produkt == null) return RedirectToAction("Liste");
            return View(produkt);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Slett(int id, Produkt endreVare)
        {

            if (ModelState.IsValid)
            {
                if (_produktBLL.slettVare(id) == false) return View();

                return RedirectToAction("Liste");
            }
            return View();
        }
    }
}

