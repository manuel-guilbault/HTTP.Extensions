using HTTP.Extensions.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP.Extensions.MVC.Caching;
using HTTP.Extensions.MVC.Ranges;
using System.IO;

namespace HTTP.Extensions.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var stream = new MemoryStream();

            var writer = new StreamWriter(stream);
            writer.Write("0123456789");
            writer.Flush();

            stream.Position = 0;

            return this.Range(stream, "text/plain");
        }
    }

}
