using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;
using VidlyTest.ViewModels;
using System.Data.Entity;
using PagedList;
using System.Data;

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
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(NewRental newRental)
        {
            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);
            var movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return RedirectToAction("New", "Rentals");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Rentals");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Rental item = _context.Rentals.Find(id);
                _context.Rentals.Remove(item);
                _context.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Details", new { id = id});
            }
            return RedirectToAction("Index");
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