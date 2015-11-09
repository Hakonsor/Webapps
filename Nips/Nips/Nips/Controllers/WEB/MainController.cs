using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Nips.BLL;
using Nips.Model.Entities;
using Nips.Domain.Entities;

namespace Nips.Controllers
{
    public class MainController : Controller
    {
        private KundeBLL _kundebll;

        public MainController()
        { 
            _kundebll = new KundeBLL();
        }

        public MainController(KundeBLL stub)
        {
            _kundebll = stub;
        }
        
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Kunde");
        }
        public ActionResult LogOut()
        {
            Session["loggedInUser"] = null;
            return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Kjøp(int id)
        {
            var repo = new ProduktBLL();

            var p = repo.hentEnVare(id);
            if ((p) != null)
            {
                System.Diagnostics.Debug.WriteLine("TODO legge til vare");
            }
            return RedirectToAction("Front");

        }
        public PartialViewResult Kategorier(String type)
        {
            var repo = new ProduktBLL();

            var liste = repo.alleVare().Where(e => e.kategori == type).FirstOrDefault();
            return PartialView("PartialVarer", liste);


        }
        public PartialViewResult Varer(int id)
        {
            var repo = new ProduktBLL();

            var vare = repo.hentEnVare(id);
            ViewBag.VareNavn = vare.navn;
            ViewBag.VareAntall = vare.antall;
            ViewBag.VarePris = vare.pris;
            ViewBag.VareBeskrivelse = vare.beskrivelse;

            return PartialView();
        }

        //Fjerne denne?
        public ViewResult LogIn(LogIn kund)
        {
            throw new NotImplementedException();
        }

        public PartialViewResult PartialVarer(Produkt type)
        {
            var repo = new ProduktBLL();

            var liste = repo.alleVare().Where(e => e.kategori == type.kategori).FirstOrDefault();
            return PartialView("PartialVarer", liste);
        }

        public ActionResult Front()
        {
            var repo = new ProduktBLL();

            var liste = repo.alleVare().ToList().Take(3).ToList();
            if (liste.Any())
            {
                return View(liste);
            }
            return View(new List<Produkt>());

        }
        public ActionResult Main()
        {
            if (!isAdmin())
                return RedirectToAction("Login", "Front");

            return View();
        }

        public ActionResult logIn(LogIn bruker)
        {
            if (ModelState.IsValid)
            {
                var admin = _kundebll.logIn(bruker.email, bruker.passord);
                if (admin != null)
                {
                    Session["loggedInUser"] = admin;
                    return RedirectToAction("Admin");
                }
            }
            return RedirectToAction("Personligside", "Kunde");       
        }

        private bool isAdmin()
        {
            if (Session == null)
                return false;
            var bruker = (Kunde)Session["loggedInUser"];
            return (bruker == null) ? false : bruker.admin;
        }
    }
}