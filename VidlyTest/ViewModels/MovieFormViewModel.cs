using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VidlyTest.Models;

namespace VidlyTest.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}