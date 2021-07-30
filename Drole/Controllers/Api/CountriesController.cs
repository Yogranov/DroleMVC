using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Drole.Dtos;
using Drole.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Drole.Controllers.Api
{
    public class CountriesController : ApiController
    {
        private ApplicationDbContext _context;

        public CountriesController() {
            _context = new ApplicationDbContext();
        }

        
        public object GetCountries(string term = null) {
            var data = _context.Countries.Where(s => term== null || s.Name.ToLower().Contains(term.ToLower())).ToList();

            var jsonToSend = "[";
            foreach (var country in data) {
                jsonToSend += "{label: " + "'" + country.Name + "'" + ", value: " + "'" + country.Id + "'},";
            }

            jsonToSend += "]";

            return JsonConvert.DeserializeObject(jsonToSend);
        }
    }
}
