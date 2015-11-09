using BLL;
using Nips.BLL;
using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Diagnostics;
using PagedList;

namespace Nips.Controllers
{
    public class KundeController : Controller
    {
        private IKundeLogikk _KundeBLL;

        public KundeController()
        {
            _KundeBLL = new KundeBLL();
        }
        public KundeController(IKundeLogikk stub)
        {
            _KundeBLL = stub;
        }
        //Her kommer man dersom man har registrert en ny bruker 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrer(Kunde newUser, String password_confirmation)
        {
            if (ModelState.IsValid)
            {
                if (!newUser.passord.Equals(password_confirmation))
                {

                    ViewBag.confirmation = true;
                    return View();
                }


                if (!_KundeBLL.checkEmail(newUser.email, null))
                {

                    ViewBag.email = true;
                    return View();
                }
                else
                {
                    byte[] hashedPassword = makeHash(newUser.passord);
                    bool insertOK = _KundeBLL.add(newUser, hashedPassword);
                    if (insertOK)
                    {
                        logInUser(newUser.email);
                        return RedirectToAction("Personligside");
                    }
                }

            }

            return View();
        }

        public ActionResult Detaljer(int id)
        {
            Kunde enKunde = _KundeBLL.getKunde(id);
            return View(enKunde);
        }

        // admin sin liste av kunde
        public ActionResult Liste()
        {
            Kunde k = (Kunde)Session["loggedInUser"];
            if (k != null && !k.admin) return RedirectToAction("Login");

            return View(_KundeBLL.getKunde());


        }
        public ActionResult Endre(int id)
        {
            Kunde k = (Kunde)Session["loggedInUser"];
            if (k != null && !k.admin) return RedirectToAction("Login");
            var kunde = _KundeBLL.getKunde(id);
            return View(kunde);
        }

        [HttpPost]
        public ActionResult Endre(int id, Kunde endreKunde)
        {

            _KundeBLL.update(id, endreKunde);
            var kunde = _KundeBLL.getKunde(id);
            return View(kunde);
        }

        public ActionResult LogOut()
        {
            Order order = new Order();
            Session["loggedInUser"] = null;
            Session["isAdmin"] = null;
            return RedirectToAction("Front", "Main");

        }

        public ActionResult Slett(int id)
        {
            Kunde k = (Kunde)Session["loggedInUser"];
            if (k != null && !k.admin) return RedirectToAction("Login");
            var kunde = _KundeBLL.getKunde(id);
            return View(kunde);
        }

        [HttpPost]
        public ActionResult Slett(int id, Kunde slettKunde)
        {

            if (_KundeBLL.slett(id))
            {
                return RedirectToAction("Liste");
            }
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }
        

        // Her skjekker kaller man først opp userExits, dersom den gjør det så er man logget inn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(String email, String passord)
        {
            var pw = passord;
            var un = email;


            var hashedPassword = makeHash(pw);
            System.Diagnostics.Debug.WriteLine(passord);
            if (_KundeBLL.validate(un, hashedPassword))
            {
                logInUser(un);
                Kunde k = (Kunde)Session["loggedInUser"];
                if (k.admin == true)
                {
                    Session["isAdmin"] = true;
                    return RedirectToAction("Admin","Order");
                }
                return RedirectToAction("Personligside");
            }
            else
            {
                Session["loggedInUser"] = null;
                ViewBag.Error = "Passord eller bruker er feil! Prøv igjen.";
                return View();
            }

        }



        public ActionResult Registrer()
        {
            return View();

        }

        //Dersom man går inn i den personlige siden så viser man denne siden, ellers må man logge inn.
        public ActionResult Personligside()
        {
            if (Session["loggedInUser"] != null)
            {
                TempData["pview"] = "none";
                Kunde k = (Kunde)Session["loggedInUser"];
                return View("Personligside", k);
            }
            return RedirectToAction("Front", "Main");
        }

        //Hasher passordet
        private static byte[] makeHash(string inPassword)
        {
            byte[] inData, outData;
            var algorithm = System.Security.Cryptography.SHA256.Create();
            inData = System.Text.Encoding.ASCII.GetBytes(inPassword);
            outData = algorithm.ComputeHash(inData);
            return outData;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateUserPassword(String op, String np, String bp)
        {
            if (Session["loggedInUser"] == null)
                return RedirectToAction("Front", "Main");
            if (ModelState.IsValid)
            {
                Kunde k = (Kunde)Session["loggedInUser"];
                byte[] hpass = makeHash(op);
                if (Enumerable.SequenceEqual(k.hashpassord, hpass))
                {
                    if (np.Equals(bp))
                    {
                        byte[] hashedPassword = makeHash(np);
                        k.hashpassord = hashedPassword;

                        bool updateOK = _KundeBLL.updatePass(k.kundeid, hashedPassword);

                        if (updateOK)
                        {
                            Session["loggedInUser"] = k;
                            TempData["changed"] = "Passord ble endret";
                            return RedirectToAction("Personligside");
                        }
                        else
                        {
                            ViewBag.correct = "klarte ikke oppdatere";

                            return View();
                        }


                    }
                    ViewBag.correct = "bekreftet ikke passordet riktig";
                    return View();
                }
                ViewBag.correct = "ikke riktig nåværende passord";
                return View();
            }
            return RedirectToAction("Personligside");
        }

        public ActionResult updateUserPassword()
        {

            Kunde k = (Kunde)Session["loggedInUser"];
            if (k == null)
                return RedirectToAction("Front", "Home");
            TempData["pview"] = "password";
            return View("Oppdaterpassord", k);
        }

        public ActionResult updateUserinfo()
        {
            Kunde k = (Kunde)Session["loggedInUser"];
            if (k == null)
                return RedirectToAction("Front", "Home");
            TempData["pview"] = "info";
            return View("Oppdaterbruker", k);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult updateUserinfo(Kunde newUser)
        {

            if (ModelState.IsValid)
            {
                Kunde k = (Kunde)Session["loggedInUser"];

                if (!_KundeBLL.checkEmail(newUser.email, k.kundeid))
                {
                    ViewBag.ok = "email er allerede registrert, velg en annen";
                    return View();
                }

                k.fornavn = newUser.fornavn;
                k.etternavn = newUser.etternavn;
                k.email = newUser.email;
                k.tlf = newUser.tlf;
                k.adresse = newUser.adresse;
                k.postnr = newUser.postnr;
                k.poststed = newUser.poststed;

                bool updateOK = _KundeBLL.update(k.kundeid, k);

                if (updateOK)
                {
                    Session["loggedInUser"] = k;
                    TempData["changed"] = "Brukerinformasjon ble oppdatert";
                    return RedirectToAction("Personligside");
                }
                else
                {
                    Kunde old = (Kunde)Session["loggedInUser"];
                    ViewBag.ok = "klarte ikke oppdatere";
                    return View();
                }
            }
            return View();
        }




        private void logInUser(String un)
        {

            var founduser = _KundeBLL.finnKunde(un);

            if (int.Parse(founduser.postnr) < 1000)
            {
                founduser.postnr = "0" + founduser.postnr;
                if (int.Parse(founduser.postnr) < 100)
                {
                    founduser.postnr = "0" + founduser.postnr;
                    if (int.Parse(founduser.postnr) < 10)
                        founduser.postnr = "0" + founduser.postnr;
                }

            }

            founduser.handlevogn = new Handlevogn(founduser.kundeid);
            Session["loggedInUser"] = founduser;
            if (Session["Anonym"] != null)
            {
                Kunde k = (Kunde)Session["loggedInUser"];
                Kunde anonym = (Kunde)Session["Anonym"];
                k.handlevogn = anonym.handlevogn;
                Session["loggedInUser"] = k;
            }

        }

        public ActionResult ListCustomers(int? page, int? itemsPerPage, string sortOrder, string currentFilter, string searchString)
        {
            if (!isAdmin())
                return RedirectToAction("Login", "Kunde");
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = sortOrder == "FName" ? "fname_desc" : "FName";
            ViewBag.LastNameSortParm = sortOrder == "LName" ? "lname_desc" : "LName";
            ViewBag.AddressSortParm = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.EMailSortParm = sortOrder == "EMail" ? "email_desc" : "EMail";
            ViewBag.PostAreaSortParm = sortOrder == "PArea" ? "parea_desc" : "PArea";
            ViewBag.PostCodeSortParm = sortOrder == "PCode" ? "pcode_desc" : "PCode";

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            List<Kunde> alleKunder;

            if (!String.IsNullOrEmpty(searchString))
            {
                alleKunder = _KundeBLL.getResult(searchString);
            }
            else
                alleKunder = _KundeBLL.getAll();

            switch (sortOrder)
            {
                case "FName":
                    alleKunder = alleKunder.OrderBy(s => s.fornavn).ToList();
                    break;
                case "fname_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.fornavn).ToList();
                    break;
                case "LName":
                    alleKunder = alleKunder.OrderBy(s => s.etternavn).ToList();
                    break;
                case "lname_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.etternavn).ToList();
                    break;
                case "Address":
                    alleKunder = alleKunder.OrderBy(s => s.adresse).ToList();
                    break;
                case "address_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.adresse).ToList();
                    break;
                case "EMail":
                    alleKunder = alleKunder.OrderBy(s => s.email).ToList();
                    break;
                case "email_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.email).ToList();
                    break;
                case "PArea":
                    alleKunder = alleKunder.OrderBy(s => s.poststed).ToList();
                    break;
                case "parea_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.poststed).ToList();
                    break;
                case "PCode":
                    alleKunder = alleKunder.OrderBy(s => s.postnr).ToList();
                    break;
                case "pcode_desc":
                    alleKunder = alleKunder.OrderByDescending(s => s.postnr).ToList();
                    break;
                default:
                    alleKunder = alleKunder.OrderBy(s => s.kundeid).ToList();
                    break;
            }

            ViewBag.CurrentItemsPerPage = itemsPerPage;

            List<Kunde> list = new List<Kunde>();
            foreach (var item in alleKunder)
            {
                list.Add(
                    new Kunde()
                    {
                        kundeid = item.kundeid,
                        fornavn = item.fornavn,
                        etternavn = item.etternavn,
                        email = item.email,
                        adresse = item.adresse,
                        tlf = item.tlf,
                        passord = item.passord,
                        hashpassord = item.hashpassord,
                        poststed = item.poststed,
                        postnr = item.postnr,
                        admin = item.admin
                    });
            }
           var kunde = alleKunder.ToPagedList(pageNumber: page ?? 1, pageSize: itemsPerPage ?? 15);
            return View(kunde);
        }

        private bool isAdmin()
        {
            if (Session == null)
            {
                Debug.WriteLine("her?");
                return false;
            }
            var user = (Kunde)Session["loggedInUser"];
            return (user == null) ? false : user.admin;
        }
    }
}
