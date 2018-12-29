using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VidlyTest.Models;

namespace VidlyTest.Controllers.Api
{
    public class ReportApiController : ApiController
    {
        private ApplicationDbContext _context;

        public ReportApiController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/customers
        public IEnumerable<string> GetFavouriteMovie()
        {
            var movies = _context.Movies
                .Include(m => m.Genre).ToList();
            var rentals = _context.Rentals.Include(c => c.Movie).Include(c => c.Customer).ToList();

            var mostPopMovies = rentals
                .GroupBy(q => q.Movie.Id)
                .OrderByDescending(gp => gp.Count())
                .Take(3)
                .Select(g => g.Key).ToList();
            List<string> movieNames = new List<string>();
            foreach (int item in mostPopMovies)
            {
                var name = movies.Single(s => s.Id == item);
                movieNames.Add(name.Name);
            }
            return movieNames;
        }
    }
}
