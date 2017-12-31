using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShootingCompetitionsRequests.Models;

namespace ShootingCompetitionsRequests.Controllers
{
    public class StandartFiltersController : Controller
    {
        public StandartFiltersController()
        { 
        }

        //
        // GET: /StandartFilters/

        public PartialViewResult GetRegionsByCountry(int idCountry, string tagName, bool addAll)
        {
            var countries = StandartClassifierModelLogic.GetRegionsByCountry(idCountry, addAll);
            var model = new ShootingCompetitionsRequests.Models.DropDownListModel
            {
                Name = tagName,
                Items = countries
            };
            return PartialView("DropDownListModel", model);
        }

    }
}
