using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nips.Model.Entities;
using Nips.Controllers;
using Nips.BLL;
using Nips.EF.Repositories.Stud;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;
using System.Collections.Generic;

namespace NipsTest
{
    [TestClass]
    public class VareControllerTest
    {

        [TestMethod]
        public void Vare_Liste_View_OK()
        {
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            var forventetListe = new List<Produkt>();
            var forventet = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            forventetListe.Add(forventet);
            forventet.id = 2;
            forventetListe.Add(forventet);
            forventet.id = 3;
            forventetListe.Add(forventet);
            forventet.id = 4;
            forventetListe.Add(forventet);

            //atc

            var result = (ViewResult)controller.Liste();
            var resultatliste = (List<Produkt>)result.Model;
            //Assets
            
            Assert.AreEqual(result.ViewName, "");
            for(var i = 0; i < resultatliste.Count; i++)
            {
                Assert.AreEqual(resultatliste[i].id, forventetListe[i].id);
                Assert.AreEqual(resultatliste[i].navn, forventetListe[i].navn);
                Assert.AreEqual(resultatliste[i].pris, forventetListe[i].pris);
            }
            
        }
        [TestMethod]
        public void Vare_Liste_View_admin_false()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            var kunde = new Kunde()
            {
                admin = false,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };

            controller.Session["loggedInUser"] = kunde;

            //atc
            var result = (RedirectToRouteResult)controller.Liste();

            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Liste_View_admin_null()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);

            //atc
            controller.Session["loggedInUser"] = null;

            var result = (RedirectToRouteResult)controller.Liste();
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }


        [TestMethod]
        public void Vare_Registerer_View_admin_true()
        {
            //Arrange
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;


            //atc

            var result = (ViewResult)controller.Registrer();
            //Assets
            Assert.AreEqual(result.ViewName, "");

        }
        [TestMethod]
        public void Vare_Registerer_View_admin_null()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);

            //atc
            controller.Session["loggedInUser"] = null;

            var result = (RedirectToRouteResult)controller.Registrer();
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Registerer_View_admin_false()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            var kunde = new Kunde()
            {
                admin = false,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };

            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (RedirectToRouteResult)controller.Registrer();
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
            
           // Assert.AreEqual(result.ViewName, "");
        }
        //post
        [TestMethod]
        public void Vare_Registerer_modelstate_invalid()
        {
            //Arrange
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            controller.ViewData.ModelState.AddModelError("pris","ikke oppgitt pris");
            //act
            var result = (ViewResult)controller.Registrer(produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);


        }
        [TestMethod]
        public void Vare_Registerer_Ok()
        {
            // Arrange
            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));

            // Atc
            //var result = (ViewResult)controller.Registrer(produkt);
            var result = (RedirectToRouteResult)controller.Registrer(produkt);

            //Assets
            Assert.AreEqual(result.RouteValues.Values.First(), ("Liste"));
            Assert.AreEqual("Liste", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Registerer_Invaliid()
        {
            var produkt = new Produkt()
            {
                id = 0,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));

            // Atc
            //var result = (ViewResult)controller.Registrer(produkt);
            var result = (ViewResult)controller.Registrer(produkt);

            //Assets
            Assert.AreEqual(result.ViewName, "");
            //Assert.AreEqual(result.ViewBag.VareFeil, false);


        }

        [TestMethod]
        public void Vare_Endre_View_OK()
        {
            int id = 1;
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (ViewResult)controller.Endre(id);
            //Assets
            var produkt = (Produkt)result.Model;
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(produkt.id , id);
        }
        [TestMethod]
        public void Vare_Endre_View_admin_false()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            int id = 1;
            var kunde = new Kunde()
            {
                admin = false,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };

            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (RedirectToRouteResult)controller.Endre(id);
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Endre_View_admin_null()
        {
            int id = 1;
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);

            //atc
            controller.Session["loggedInUser"] = null;

            var result = (RedirectToRouteResult)controller.Endre(id);
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Endre_View_Finnes_ikke()
        {
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            int id = 2;

            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (RedirectToRouteResult)controller.Endre(id);

            //Assets
            Assert.AreEqual(result.RouteValues.Values.First(), ("Liste"));
            Assert.AreEqual("Liste", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Vare_Endre_modelstate_invaid()
        {
            //Arrange
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            int id = 1;
            controller.ViewData.ModelState.AddModelError("pris", "ikke oppgitt pris");

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (ViewResult)controller.Endre(id, produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        }
        [TestMethod]
        public void Vare_Endre_finnes_ikke()
        {
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            int id = 2;

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (ViewResult)controller.Endre(id, produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.Model == null);

        }
        [TestMethod]
        public void Vare_Endre_finnes()
        {
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            int id = 1;

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (ViewResult)controller.Endre(id, produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(id, ((Produkt)result.Model).id);
        }

        [TestMethod]
        public void Vare_Slett_View_OK()
        {
            int id = 1;
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (ViewResult)controller.Slett(id);
            //Assets
            var produkt = (Produkt)result.Model;
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(produkt.id, id);
        }
        [TestMethod]
        public void Vare_Slett_View_admin_false()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            int id = 1;
            var kunde = new Kunde()
            {
                admin = false,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };

            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (RedirectToRouteResult)controller.Slett(id);
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Slett_View_admin_null()
        {
            int id = 1;
            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);

            //atc
            controller.Session["loggedInUser"] = null;

            var result = (RedirectToRouteResult)controller.Slett(id);
            //Assets
            Assert.AreEqual("LogIn", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Slett_View_Finnes_ikke()
        {
            var kunde = new Kunde()
            {
                admin = true,
                adresse = "",
                email = "",
                fornavn = "Håkon",
                etternavn = "Sørby",
                passord = "123",
                tlf = "123",
                postnr = "",
                poststed = "",
                kundeid = 1
            };
            int id = 2;

            var SessionMock = new TestControllerBuilder();
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            //atc

            var result = (RedirectToRouteResult)controller.Slett(id);

            //Assets
            Assert.AreEqual(result.RouteValues.Values.First(), ("Liste"));
            Assert.AreEqual("Liste", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Vare_Slett_modelstate_invaid()
        {
            //Arrange
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            int id = 1;
            controller.ViewData.ModelState.AddModelError("pris", "ikke oppgitt pris");

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (ViewResult)controller.Slett(id, produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        }
        [TestMethod]
        public void Vare_Slett_finnes_ikke()
        {
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            int id = 2;

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (ViewResult)controller.Slett(id, produkt);

            //assets
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.Model == null);

        }
        [TestMethod]
        public void Vare_Slett_finnes()
        {
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));

            var produkt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            //act
            var result = (RedirectToRouteResult)controller.Registrer(produkt);

            //Assets
            Assert.AreEqual(result.RouteValues.Values.First(), ("Liste"));
            Assert.AreEqual("Liste", result.RouteValues["Action"]);
        }
        [TestMethod]
        public void Vare_Details_View_OK()
        {
            int id = 1;

            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));

            //atc

            var result = (ViewResult)controller.Details(id);
            //Assets
            var produkt = (Produkt)result.Model;
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(produkt.id, id);
        }
        [TestMethod]
        public void Vare_Details_View_Finnes_ikke()
        {

            int id = 2;
            var controller = new VareController(new ProduktBLL(new ProduktRepositoryStud()));
            //atc
            var result = (RedirectToRouteResult)controller.Details(id);
            //Assets
            Assert.AreEqual(result.RouteValues.Values.First(), ("Liste"));
            Assert.AreEqual("Liste", result.RouteValues["Action"]);
        }

    }


}
