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
    public class MoviesController : Controller
    {
        //get Db context
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            //add movies and their relations(//Genre) from database
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);

        }
        public ActionResult Details(int id)
        {
            //add a movie and its relations(//Genre) from database
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            return View(movie);
        }
    }
}