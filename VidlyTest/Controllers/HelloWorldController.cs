using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;

namespace VidlyTest.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult HelloWorld()
        {
            var movie = new Movie() { Name = "Shrek" };
            return View(movie);
        }
    }
}