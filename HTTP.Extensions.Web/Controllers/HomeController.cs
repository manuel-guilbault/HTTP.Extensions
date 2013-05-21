using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTP.Extensions.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var tags = new EntityTagParser().Parse(@"""toto"", W/""xyzzy""");

            return View();
        }
    }
}
