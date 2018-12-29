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
    public class MoviesApiController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesApiController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/customers
        public IEnumerable<Movie> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesList = moviesQuery.ToList();

            return moviesList;
        }
       
    }
}
