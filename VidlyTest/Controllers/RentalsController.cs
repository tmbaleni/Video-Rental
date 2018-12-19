using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;
using VidlyTest.ViewModels;
using System.Data.Entity;

namespace VidlyTest.Controllers
{
    public class RentalsController : Controller
    {
        //get Db context
        private ApplicationDbContext _context;
        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Rentals
        public ActionResult Index()
        {
            var rentals = _context.Rentals.Include(c => c.Movie).Include(c => c.Customer).ToList();
            return View(rentals);
        }
    }
}