using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Drole.Controllers {
    public class HomeController : ApplicationBaseController {
        public ActionResult Index() {
            return View();
        }

        public ActionResult About() {
            return View();
        }

        public ActionResult Contact() {
            return View();
        }

        public ActionResult LegalInformation() {
            return View();
        }

        public ActionResult TermsOfUse() {
            return View();
        }

    }
}