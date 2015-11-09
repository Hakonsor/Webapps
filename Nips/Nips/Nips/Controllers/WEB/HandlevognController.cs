using BLL;
using Nips.BLL;
using Nips.Model.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nips.Controllers
{
    public class HandlevognController : Controller
    {
        
            private IOrderLogikk _OrdereBLL;

            public HandlevognController()
            {

            _OrdereBLL = new OrdereBLL();
            }

            public HandlevognController(IOrderLogikk stub)
            {
            _OrdereBLL = stub;
            }

            public ActionResult Handlevogn()
            {
                Handlevogn vogn = getVogn();
                return View(vogn);
            }

            [HttpPost]
            public ActionResult addToVogn(int antall, int varenummer)
            {
                var repo = new ProduktBLL();

                var p = repo.hentEnVare(varenummer);
                Handlevogn vogn = getVogn();
                if (vogn == null) vogn = nyVogn();
                HandlevognVare vare = new HandlevognVare(p, antall);
                List<HandlevognVare> list = vogn.handlevognVare;
                vogn.sum += p.pris * antall;
                list.Add(vare);
                return RedirectToAction("Handlevogn");

            }

            private Handlevogn nyVogn()
            {
                Kunde kund;
                if (Session["LoggdInUser"] != null)
                {
                    kund = (Kunde)Session["LoggedInUser"];
                }
                else
                {
                    kund = new Kunde();
                    kund.kundeid = Int32.MaxValue - 1;
                    kund.fornavn = "Anonym";
                    Session["Anonym"] = kund;
                }
                if (getVogn() == null)
                {
                    kund.handlevogn = new Handlevogn(kund.kundeid);

                }
                return kund.handlevogn;
            }


            public ActionResult updateVogn(int id, int antall)
            {
                if (id != 0 && antall != 0)
                {
                    Kunde kund = (Kunde)Session["LoggedInUser"];
                    Handlevogn vogn = kund.handlevogn;
                    vogn.sum -= vogn.handlevognVare[id].pris;
                    vogn.handlevognVare[id].pris = vogn.handlevognVare[id].produkt.pris * antall;
                    vogn.handlevognVare[id].antall = antall;

                    vogn.sum += vogn.handlevognVare[id].pris;
                    Session["LoggedInUser"] = kund;
                    return RedirectToAction("Handlevogn");
                }
                return RedirectToAction("Handlevogn");
            }

            public ActionResult removeItem(int id)
            {
                Kunde kund = (Kunde)Session["LoggedInUser"];
                Handlevogn vogn = kund.handlevogn;
                vogn.sum -= vogn.handlevognVare[id].pris;
                vogn.handlevognVare.RemoveAt(id);
                Session["LoggedInUser"] = kund;
                return RedirectToAction("Handlevogn");
            }

            private Handlevogn getVogn()
            {
                if (Session["loggedInUser"] != null)
                {
                    Kunde k = (Kunde)Session["loggedInUser"];
                    Handlevogn vogn = k.handlevogn;
                    return vogn;
                }
                else if (Session["Anonym"] != null)
                {
                    Kunde k = (Kunde)Session["Anonym"];
                    Handlevogn vogn = k.handlevogn;
                    return vogn;
                }
                else
                {
                    var kund = new Kunde();
                    kund.kundeid = Int32.MaxValue - 1;
                    kund.fornavn = "Anonym";
                    kund.handlevogn = new Handlevogn(kund.kundeid);
                    Session["Anonym"] = kund;

                    return kund.handlevogn;
                }

            }

            public ActionResult Checkout()
            {

                if (Session["Anonym"] == null && Session["LoggedInUser"] == null)
                {
                    return RedirectToAction("Front", "Main");
                }

                if (Session["Anonym"] == null && Session["LoggedInUser"] == null)
                {
                    ViewBag.LoginFeil = true;
                    return RedirectToAction("Handlevogn");
                }

                var vogn = getVogn();
                var liste = vogn.handlevognVare;
                if (liste.Count == 0)
                {
                    return RedirectToAction("Handlevogn"); // denne derper
                }


                ViewBag.Kunde = (Kunde)Session["LoggedInUser"];
                return View();
            }

            [HttpPost]
            public ActionResult Checkout(Kvittering kvit)
            {
                return RedirectToAction("Kvittering");
            }

            public ActionResult Kvittering()
            {

                bool Godkjenning = true;
                if (Godkjenning == false)
                {
                    ViewBag.IkkeGodKjent();
                    return RedirectToAction("Checkout");
                }
                ViewBag.Empty = false;

                Handlevogn vogn = getVogn();
                if (vogn == null || vogn.handlevognVare == null)
                {
                    ViewBag.Empty = true;

                    return RedirectToAction("Checkout");
                }

                var returnKunde = (Kunde)Session["LoggedInUser"];
                vogn.userID = returnKunde.kundeid;

                var orderId = _OrdereBLL.checkout(vogn);
                if (orderId == -1) return RedirectToAction("Handlevogn");

                returnKunde.handlevogn = new Handlevogn(returnKunde.kundeid);
                Session["LoggedInUser"] = returnKunde;

                var kvittering = new Kvittering()
                {
                    kunde = returnKunde,
                    handlevogn = vogn,
                    order = new Order()
                    {
                        id = orderId,
                        kundeid = returnKunde.kundeid,
                        orderdato = DateTime.Now
                    },
                    sum = vogn.sum,
                    exmva = vogn.sum * 0.8,
                    mva = vogn.sum * 0.2
                };

                return View(kvittering);


            
        }
    }
}
