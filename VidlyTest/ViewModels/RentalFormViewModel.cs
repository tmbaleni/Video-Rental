using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using VidlyTest.Models;

namespace VidlyTest.ViewModels
{
    public class RentalFormViewModel
    {
        public Customer Customer { get; set; }

        public Movie Movie { get; set; }

        public int? Id { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

        public RentalFormViewModel()
        {
            Id = 0;
        }
        public RentalFormViewModel(Rental rental)
        {
            Id = rental.Id;
            Customer = rental.Customer;
            Movie = rental.Movie;
            DateRented = rental.DateRented;
            DateReturned = rental.DateReturned;
        }
    }
}