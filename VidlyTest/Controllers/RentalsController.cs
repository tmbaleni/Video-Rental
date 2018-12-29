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
        [Authorize(Roles = "StoreManager")]
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
        public ActionResult Return(int id)
        {
                var rentalInDb = _context.Rentals.Include(r => r.Movie).Include(r => r.Customer).SingleOrDefault(m => m.Id == id);
            if (rentalInDb == null)
            {
                return HttpNotFound();
            }
            else
            {
                rentalInDb.DateReturned = DateTime.Now;
                var movieId = rentalInDb.Movie.Id;
                var movieInDb = _context.Movies.Single(m => m.Id == movieId);
                movieInDb.NumberAvailable--;
                //_context.Rentals.SqlQuery();

            }
                _context.SaveChanges();

            return RedirectToAction("Index","Home");
        }
        [Authorize(Roles = "StoreManager")]
        public ActionResult Report()
        {
            List<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            var membershipTypeCountsQuery =
            from p in customers
            group p by p.MembershipTypeId into g
            select new { CategoryMembershipType = g.Key, Count = g.Count() };

            var membershipTypeCounts = membershipTypeCountsQuery.ToList();
            var listCount = membershipTypeCounts.Count();

            List<Report> reportList = new List<Report>();
                
                
            for (int i = 0; i < listCount; i++)
            {
                var item = new Report
                {
                    CategoryMembershipType = membershipTypeCounts[i].CategoryMembershipType,
                    Count = membershipTypeCounts[i].Count
                };
                reportList.Add(item);
                
            }

            return View("Report", reportList);
        }

        }
}