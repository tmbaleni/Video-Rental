using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VidlyTest.Models;

namespace VidlyTest.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index()
        {
            ViewBag.Message = "Hello from customers controller";
            var customers = GetCustomers();
            return View(customers);
        }
        public ActionResult Details(int id)
        {
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            return View(customer);
        }
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Name = "Tabo Mbaleni" },
                new Customer { Id = 2, Name = "Mary Williams" },
                new Customer { Id = 3, Name = "Jane Doe" },
                new Customer { Id = 4, Name = "Serena Williams" }
            };
        }
    }
}