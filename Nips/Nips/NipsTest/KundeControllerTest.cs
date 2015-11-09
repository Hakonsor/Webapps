using System.Web.Mvc;
using Nips.Controllers;
using Nips.BLL;
using Nips.Domain.Entities;
using MvcContrib.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nips.DAL.Repositories.Stud;
using System.Linq;
using Nips.Model.Entities;
using System.Collections.Generic;

namespace NipsTest
{
    [TestClass]
    public class KundeControllerTest
    {
        [TestMethod]
        public void Kunde_Reg()
        {
            // Arrange
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));

            //Act
            var result = (ViewResult)controller.Registrer();

            //Assert
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(null, result.Model);
           
        }

        [TestMethod]
        public void Registrer_OK()
        {
            // Arrange
           
            var SessionMock = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            SessionMock.InitializeController(controller);

            //atc
            controller.Session["loggedInUser"] = null;
            var kund = new Kunde()
            {
                kundeid = 2,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };
            // Act
            var result = (RedirectToRouteResult)controller.Registrer(kund, kund.passord);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "Personligside");
        }

        [TestMethod]
        public void Registrer_feil()
        {
            // Arrange
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            var kund = new Kunde()
            {
                kundeid = 2,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };
            controller.ViewData.ModelState.AddModelError("fornavn", "Ikke oppgitt fornavn!!!");

            // Act
            var result = (ViewResult)controller.Registrer(kund, kund.passord);

            // Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.IsTrue(result.ViewData.ModelState.Count == 1);
        }

        [TestMethod]
        public void Registrer_Post_DB_feil()
        {
            // Arrange
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };

            // Act
            var result = (ViewResult)controller.Registrer(kund, kund.passord);

            // Assert
            Assert.AreEqual(result.ViewName, "");
            Assert.AreEqual(null, result.Model);
        }



        [TestMethod]
        public void Kunde_Ikke_Admin()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            var SessionMock = new TestControllerBuilder();
            
            SessionMock.InitializeController(controller);
            controller.Session["isAdmin"] = null;

            var kund = new Kunde()
            {
                admin = false,
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "lala@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };

            //Act
            var actionResult = (ViewResult)controller.LogIn(kund.email, kund.passord);

            // Assert
            Assert.AreEqual(actionResult.ViewName,"", "Personligside");
        }

        [TestMethod]
        public void Kunde_Er_Admin()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            var SessionMock = new TestControllerBuilder();

            SessionMock.InitializeController(controller);
            controller.Session["isAdmin"] = null;

                var kund = new Kunde() 
{
                admin = true, 
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "lala@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
                };
            
            //Act
            var actionResult = (ViewResult)controller.LogIn(kund.email, "12345678");

            // Assert
            Assert.AreEqual(actionResult.ViewName, "", "Personligside");
        }


        [TestMethod]
        public void List_Kunder()
        {
            //Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var Liste = new List<Kunde>();
            var kunde = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };

            Liste.Add(kunde);
            Liste.Add(kunde);
            Liste.Add(kunde);

            //Act
            var actionResult = (ViewResult)controller.Liste();
            var resultat = (List<Kunde>)actionResult.Model;

            // Assert
            Assert.IsNotNull(resultat);
            Assert.IsTrue(resultat[0].kundeid < resultat[1].kundeid);
        }

        [TestMethod]
        public void Detaljer()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var fresultat = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };
            // Act
            var actionResult = (ViewResult)controller.Detaljer(1);
            var resultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(fresultat.kundeid, resultat.kundeid);
            Assert.AreEqual(fresultat.fornavn, resultat.fornavn);
            Assert.AreEqual(fresultat.etternavn, resultat.etternavn);
            Assert.AreEqual(fresultat.adresse, resultat.adresse);
            Assert.AreEqual(fresultat.email, resultat.email);
            Assert.AreEqual(fresultat.postnr, resultat.postnr);
            Assert.AreEqual(fresultat.poststed, resultat.poststed);
            Assert.AreEqual(fresultat.tlf, resultat.tlf);
            Assert.AreEqual(fresultat.passord, resultat.passord);
        }

        [TestMethod]
        public void Endre_Kunde()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };

            // Act
            var actionResult = (ViewResult)controller.Endre(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_Finnes_Ikke()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };

            // Act
            var actionResult = (ViewResult)controller.Endre(0);
            var kundeResultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(kundeResultat.kundeid, 0);
        }

        [TestMethod]
        public void Endre_ikke_funnet_()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };

            // Act
            var actionResult = (ViewResult)controller.Endre(0, kund);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Endre_funnet()
        {
            //Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };
            // Act
            var actionResult = (ViewResult)controller.Endre(1, kund);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Slett_Kunde()
        {
            //Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };

            // Act
            var actionResult = (ViewResult)controller.Slett(1);
            var resultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Slett_Finnes_Ikke()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };

            // Act
            var actionResult = (ViewResult)controller.Slett(0);
            var kundeResultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(kundeResultat.kundeid, 0);
        }

        [TestMethod]
        public void Slett_ikke_funnet_()
        {
            // Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };

            // Act
            var actionResult = (ViewResult)controller.Slett(0, kund);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Slett_funnet()
        {
            //Arrange
            TestControllerBuilder builder = new TestControllerBuilder();
            var controller = new KundeController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            builder.HttpContext.Session["isAdmin"] = new Kunde() { kundeid = 1, admin = true };
            var kund = new Kunde()
            {
                kundeid = 1,
                fornavn = "Per",
                etternavn = "Hansen",
                adresse = "Per per",
                email = "per@hotmail.com",
                poststed = "1234",
                postnr = "1234",
                tlf = "12345678",
                passord = "12345678"
            };
            // Act
            var actionResult = (ViewResult)controller.Slett(0);
            var kundeResultat = (Kunde)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(kundeResultat.kundeid, 0);
        }
    }
}
