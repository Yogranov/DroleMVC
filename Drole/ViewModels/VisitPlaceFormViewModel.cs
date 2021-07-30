using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drole.Models;

namespace Drole.ViewModels {
    public class VisitPlaceFormViewModel {

        public VisitPlace VisitPlace { get; set; }

        public int CountryId { get; set; }

        public HttpPostedFileBase IconImageFile { get; set; }

        public string Title() {
            return VisitPlace.Id != 0 ? "עריכה" : "חדש";
        }
            
        public string SubTitle() {
            return VisitPlace.Id != 0 ? "עריכת" : "הוספת";
        }
    }
}