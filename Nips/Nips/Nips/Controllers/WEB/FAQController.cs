using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nips.Controllers.WEB
{
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult FAQ()
        {
            ViewBag.alfa = "@";
            return View();
        }
    }
}