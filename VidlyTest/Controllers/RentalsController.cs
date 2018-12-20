using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;
using VidlyTest.ViewModels;
using System.Data.Entity;
using PagedList;

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
        public ViewResult Index(string sortOrder, string currentFilter ,string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var rentals = from r in _context.Rentals.Include(c => c.Movie).Include(c => c.Customer)
                          select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                rentals = rentals.Where(r => r.Customer.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    rentals = rentals.OrderByDescending(r => r.Customer.Name );
                    break;
                default:
                    rentals = rentals.OrderBy( r => r.Customer.Name);
                    break;
            }

            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(rentals.ToPagedList(pageNumber, pageSize));
        }
        /*[HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            var rentals = _context.Rentals.Include(c => c.Movie).Include(c => c.Customer).ToList();
            var viewModel = new SearchViewModel()
            {
                Genres = _context.Genres.ToList()
            };

            return View("Index",viewModel);
        }*/
    }
}