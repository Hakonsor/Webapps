using System;
using System.Web.Mvc;
using Nips.Controllers;
using Nips.BLL;
using Nips.Domain.Entities;
using MvcContrib.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nips.DAL.Repositories.Stud;
using System.Linq;
using Nips.DAL.Repositories;
using Nips.Model.Entities;
using System.Collections.Generic;

namespace NipsTest
{
    [TestClass]
    public class OrderControllerTest
    {
        [TestMethod]
        public void ListOrdre_Liste_ikke_admin()
        {

        }
        [TestMethod]
        public void ListOrdre_Liste_Ok()
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
            var controller = new OrderController(new OrdereBLL(new OrderRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["loggedInUser"] = kunde;

            var forventetListe = new List<Order>();
            var orderProdukt = new Produkt()
            {
                id = 1,
                navn = "pedobear",
                antall = 5,
                beskrivelse = "varm og god",
                kategori = "Barenevennelig",
                pris = 3000,
                ImageUrl = "Trololo"
            };
            var orderline = new OrderLine()
            {
                id = 1,
                antall = 1,
                orderid = 1,
                produkt = orderProdukt,
                produktid = 1,
                


            };
            var orderliner = new List<OrderLine>();
            orderliner.Add(orderline);
            var forventetorder = new Order()
            {
                id = 1,
                kundeid = 1,
                orderdato = new DateTime(2014, 1, 2),
                orderline = orderliner,
                sendt = false
                

            };
            forventetListe.Add(forventetorder);
            forventetorder.id = 2;
            forventetorder.orderline.FirstOrDefault().id = 2;
            forventetListe.Add(forventetorder);

            //glemte å se om noen hadde implementert studen xD, bruker det isteden

            var orderliste = new List<Order>();
            var order = new Order()
            {
                id = 1,
                orderdato = new DateTime(2014, 1, 2),
                kundeid = 3,
                sendt = true,

            };
            orderliste.Add(order);
            orderliste.Add(order);
            orderliste.Add(order);


            //atc

            var result = (ViewResult)controller.Admin();
            var resultatliste = (List<Order>)result.Model;
            //Assets

            Assert.AreEqual(result.ViewName, "");
            for (var i = 0; i < resultatliste.Count; i++)
            {
                Assert.AreEqual(orderliste[i].id, resultatliste[i].id);
                Assert.AreEqual(orderliste[i].kundeid, resultatliste[i].kundeid);
               
            }

        }
    
    }
}
