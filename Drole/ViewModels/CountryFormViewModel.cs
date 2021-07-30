using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drole.Models;

namespace Drole.ViewModels {
    public class CountryFormViewModel {

        public Country Country { get; set; }
        public List<string> Laws { get; set; }
        public HttpPostedFileBase IconImageFile { get; set; }
        public HttpPostedFileBase BackgroundImageFile { get; set; }

        public string Title() {
            return Country.Id != 0 ? "עריכה" : "חדש";
        }
            
        public string SubTitle() {
            return Country.Id != 0 ? "עריכת" : "יצירת";
        }

        
    }


}