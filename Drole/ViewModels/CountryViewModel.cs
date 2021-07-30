using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drole.Models;

namespace Drole.ViewModels {
    public class CountryViewModel {

        public Country Country { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> Laws { get; set; }
        public List<VisitPlace> VisitPlaces { get; set; }

    }
}