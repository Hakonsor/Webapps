using System;
using System.Web.Mvc;
using System.Collections.Generic;
using Nips.BLL;
using Nips.Controllers;
using Nips.DAL.Repositories.Stud;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nips.Model.Entities;
using MvcContrib.TestHelper;
using BLL;

namespace NipsTest
{

    [TestClass]
    public class OrderTest
    {

        [TestMethod]
        public void test_Kvittering_vis()
        {
            //Arrange
            var controller = new HandlevognController(new OrdereBLL(new OrderRepositoryStub()));

            var forventetResultat = new List<Kvittering>();
            var forventetKvittering = new Kvittering()
            {
                /*id = 1,
                orderdato = new DateTime(2014, 1, 2),
                kundeid = 3,
                sendt = true,
                */
                kunde = new Kunde(),
                handlevogn = new Handlevogn(1)
                {
                    userID = 1,
                    handlevognVare = new List<HandlevognVare>(),
                    sum = 0


                },
                order = new Order()
                {
                    id = 1,
                    kundeid = 1,
                    orderdato = new DateTime(2014, 1, 2),
                },
                sum = 100,
                exmva = 200.0,
                mva = 300.0
            };



            var kund = new Kunde()
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

            kund.handlevogn = new Handlevogn(1)
            {
                userID = 1,
                handlevognVare = new List<HandlevognVare>(),
                sum = 0,
            };

            var SessionMock = new TestControllerBuilder();
            //var controller = new HandlevognController(new OrdereBLL(new OrderRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kund;

            //controller.ViewData.ModelState.AddModelError("pris", "ikke oppgitt pris");
            //act
           
            var actionResult = (ViewResult)controller.Kvittering();
            var resultat = (Kvittering)actionResult.Model;

            Assert.AreEqual(0, resultat.handlevogn.sum);

        }


       /* [TestMethod]
        public void test_getOrder() {

            //controller
            //var controller = new HandlevognController(new OrdereBLL(new OrderRepositoryStub()));

            var b = new OrderRepositoryStub();
            var forventetResultat = new List<Order>();
            var order = new Order()
            {
                id = 1,
                orderdato = new DateTime(2014, 1, 2),
                kundeid = 3,
                sendt = true

            };

            var SessionMock = new TestControllerBuilder();
            var controller = new HandlevognController(new OrdereBLL(new OrderRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = null;

            forventetResultat.Add(order);
            forventetResultat.Add(order);
            forventetResultat.Add(order);

            var actionResult = (ViewResult)controller.Handlevogn();
            var resultat = (List<Order>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(forventetResultat[i].id, resultat[i].id);
                Assert.AreEqual(forventetResultat[i].orderdato, resultat[i].orderdato);
                Assert.AreEqual(forventetResultat[i].kundeid, resultat[i].kundeid);
                Assert.AreEqual(forventetResultat[i].sendt, resultat[i].sendt);
            }
        }*/
        //[TestMethod]


    }
}

