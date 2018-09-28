using System.Web.Mvc;
using ShootingCompetitionsRequests.Models;

namespace ShootServ.Controllers
{
    public class StandartFiltersController : Controller
    {
        //
        // GET: /StandartFilters/

        public PartialViewResult GetRegionsByCountry(int idCountry, string tagName, bool addAll)
        {
            var countries = StandartClassifierModelLogic.GetRegionsByCountry(idCountry, addAll);
            var model = new DropDownListModel
            {
                Name = tagName,
                Items = countries
            };
            return PartialView("DropDownListModel", model);
        }

    }
}
