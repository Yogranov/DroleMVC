using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Drole.Models;
using Drole.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;

namespace Drole.Controllers {
    public class CountriesController : ApplicationBaseController {
        // GET
        private ApplicationDbContext _context;

        private CountryViewModel CountryViewModel;

        public CountriesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing) {
            _context.Dispose();
        }

        public ActionResult Index() {
            return RedirectToAction("Index","Home");
        }

        public ActionResult Country(int id) {
            var country = _context.Countries.Single(c => c.Id == id);

            CountryViewModel = new CountryViewModel() {
                Country = country,
                User = _context.Users.Single(u => u.Id == country.User),
                Laws = _context.Countries.Single(c => c.Id == id).Laws.Split(':').ToList(),
                VisitPlaces = _context.VisitPlaces.Where(v => v.Country == id).ToList()
            };

            return View(CountryViewModel);
        }

        [Authorize]
        public ActionResult New() {
            var viewModel = new CountryFormViewModel {
                Country = new Country(),
                Laws = new List<string>()
            };

            return View("Form" ,viewModel);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CountryFormViewModel countryFormViewModel) {

            if (!ModelState.IsValid)
                return View("Form", countryFormViewModel);

            string userId = User.Identity.GetUserId();

            var folder = Server.MapPath("~/Content/Uploads/" + countryFormViewModel.Country.EnglishName);
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }

            if (countryFormViewModel.IconImageFile != null) {
                var iconImagePath = Path.Combine(Server.MapPath("~/Content/Uploads/" + countryFormViewModel.Country.EnglishName), "IconImage.jpg");
                countryFormViewModel.IconImageFile.SaveAs(iconImagePath);
            }

            if (countryFormViewModel.BackgroundImageFile != null) {
                var backGroundImagePath = Path.Combine(Server.MapPath("~/Content/Uploads/" + countryFormViewModel.Country.EnglishName), "BackgroundImage.jpg");
                countryFormViewModel.BackgroundImageFile.SaveAs(backGroundImagePath);
            }

            var country = new Country() {
                Name = countryFormViewModel.Country.Name,
                EnglishName = countryFormViewModel.Country.EnglishName,
                Laws = String.Join(":", countryFormViewModel.Laws),
                User = _context.Users.Single(u => u.Id == userId).Id,
                ImageUrl = "../../Content/Uploads/" + countryFormViewModel.Country.EnglishName + "/IconImage.jpg",
                BackgroundImageUrl = "../../Content/Uploads/" + countryFormViewModel.Country.EnglishName + "/BackgroundImage.jpg"
            };
            
            if(countryFormViewModel.Country.Id == 0)
                _context.Countries.Add(country);
            else {
                var countryInDb = _context.Countries.Single(c => c.Id == countryFormViewModel.Country.Id);
                countryInDb.Name = country.Name;
                countryInDb.EnglishName = country.EnglishName;
                countryInDb.Laws = country.Laws;
                countryInDb.User = country.User;
                countryInDb.ImageUrl = country.ImageUrl;
                countryInDb.BackgroundImageUrl = country.BackgroundImageUrl;
            }
            _context.SaveChanges();
            int id = countryFormViewModel.Country.Id != 0 ? countryFormViewModel.Country.Id : country.Id;
            return RedirectToAction("country", "Countries", new {id = id});
        }


        public ActionResult Edit(int id) {
            var country = _context.Countries.Single(c => c.Id == id);
            var laws = country.Laws.Split(':').ToList();
            var viewModel = new CountryFormViewModel {
                Country = country,
                Laws = laws
            };

            return View("Form" ,viewModel);
        }
    }
}