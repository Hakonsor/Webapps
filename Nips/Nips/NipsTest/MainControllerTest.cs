using System.Web.Mvc;
using Nips.Controllers;
using Nips.BLL;
using Nips.Domain.Entities;
using MvcContrib.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nips.DAL.Repositories.Stud;
using System.Linq;

namespace NipsTest
{
    [TestClass]
    public class MainControllerTest
    {
        [TestMethod]
        public void Main_Login_View()
        {
      
            // Arrange 
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            // Act
            var result = (RedirectToRouteResult)controller.Login();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Main_Login_Invalid()
        {
            //Arrange
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            controller.ViewData.ModelState.AddModelError("Opps", "Her skjedde det noe");
            LogIn kund = new LogIn()
            {
                email = "",
                passord = ""
            };
            //Act
            var result = (RedirectToRouteResult)controller.Login();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Main_Login_OK_Admin_Null()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange 
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            LogIn kund = new LogIn()
            {
                email = "per@hotmail.com",
                passord = "12345678"
            };
            // Act
            var result = (RedirectToRouteResult)controller.logIn(kund);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual(result.RouteValues.Values.First(), ("Personligside"));         
        }

        [TestMethod]
        public void Main_Login_Ikke_Admin()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            // Arrange 
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);
            LogIn kund = new LogIn()
            {
                email = "lala@hotmail.com",
                passord = "12345678"
            };
            // Act
            var result = (RedirectToRouteResult)controller.logIn(kund);

            // Assert
            Assert.AreEqual("", result.RouteName);
            Assert.AreEqual(result.RouteValues.Values.First(), ("Personligside"));
        }

        [TestMethod]
        public void Main_login_redirect_to_main_login_OK()
        {
            TestControllerBuilder builder = new TestControllerBuilder();

            // Arrange 
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);

            LogIn kund = new LogIn()
            {
                email = "lala@hotmail.com",
                passord = "12345678"
            };
            var result = (RedirectToRouteResult)controller.logIn(kund);

            // Assert
            Assert.AreEqual("Personligside", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void Main_logout()
        {
            TestControllerBuilder builder = new TestControllerBuilder();
            //Arrange
            var controller = new MainController(new KundeBLL(new KundeRepositoryStud()));
            builder.InitializeController(controller);

            //Act
            var result = (RedirectToRouteResult)controller.LogOut();

            //Assert
            Assert.AreEqual("Login", result.RouteValues["Action"]);
        }
    }
}
