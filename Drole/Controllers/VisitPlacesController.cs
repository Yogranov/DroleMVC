using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drole.Models;
using Drole.ViewModels;

namespace Drole.Controllers
{
    public class VisitPlacesController : ApplicationBaseController
    {
        private ApplicationDbContext _context;

        public VisitPlacesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        // GET: VisitPlaes
        public ActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        [Authorize]
        public ActionResult New(int countryId) {
            var visitPlaceFormViewModel = new VisitPlaceFormViewModel() {
                VisitPlace = new VisitPlace(),
                CountryId = countryId
            };
            return View("Form" ,visitPlaceFormViewModel);
        }

        [Authorize]
        public ActionResult Edit(int id) {
            var visitPlace = _context.VisitPlaces.Single(v => v.Id == id);

            var visitPlaceFormViewModel = new VisitPlaceFormViewModel() {
                VisitPlace = visitPlace,
                CountryId = visitPlace.Country
            };
            return View("Form" ,visitPlaceFormViewModel);
        }


        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Save(VisitPlaceFormViewModel visitPlaceFormViewModel) {

            if (!ModelState.IsValid)
                return View("Form", visitPlaceFormViewModel);

            var country = _context.Countries.Single(c => c.Id == visitPlaceFormViewModel.CountryId);

            var folder = Server.MapPath("~/Content/Uploads/" + country.EnglishName);
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }

            if (visitPlaceFormViewModel.IconImageFile != null) {
                var iconImagePath = Path.Combine(Server.MapPath("~/Content/Uploads/" + country.EnglishName), visitPlaceFormViewModel.IconImageFile.FileName);
                visitPlaceFormViewModel.IconImageFile.SaveAs(iconImagePath);
            }

            var visitPlace = new VisitPlace() {
                Country = visitPlaceFormViewModel.CountryId,
                Name = visitPlaceFormViewModel.VisitPlace.Name,
                Content = visitPlaceFormViewModel.VisitPlace.Content
            };

            if (visitPlaceFormViewModel.IconImageFile != null)
                visitPlace.IconUrl = "../../Content/Uploads/" + country.EnglishName + "/" + visitPlaceFormViewModel.IconImageFile.FileName;
            else {
                visitPlace.IconUrl = _context.VisitPlaces.Single(v => v.Id == visitPlaceFormViewModel.VisitPlace.Id).IconUrl;
            }

            if (visitPlaceFormViewModel.VisitPlace.Id == 0) {
                _context.VisitPlaces.Add(visitPlace);
            } else {
                var visitPlaceInDb = _context.VisitPlaces.Single(v => v.Id == visitPlaceFormViewModel.VisitPlace.Id);
                visitPlaceInDb.Name = visitPlace.Name;
                visitPlaceInDb.Country = visitPlace.Country;
                visitPlaceInDb.Content = visitPlace.Content;
                visitPlaceInDb.IconUrl = visitPlace.IconUrl;
            }

            _context.SaveChanges();
            
            return RedirectToAction("country", "Countries", new {id = visitPlaceFormViewModel.CountryId});
        }
    }
}