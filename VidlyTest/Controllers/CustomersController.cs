using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;
using VidlyTest.ViewModels;

namespace VidlyTest.Controllers
{
    public class CustomersController : Controller
    {
        //get Db context
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult Index()
        {
            //add customers and their relations(//membershipType) from database
            var customers = _context.Customers.Include( c => c.MembershipType).ToList();
            return View(customers);
        }
        public ActionResult Details(int id)
        {
            //add a customer and his/her relations(//membershipType) from database
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        public ActionResult New()
        {
            //add membershipTypes and their relations(//name) from database
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            
            return View();
        }
    }
}