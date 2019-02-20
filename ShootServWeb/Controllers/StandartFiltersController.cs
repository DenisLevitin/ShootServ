using System.Web.Mvc;
using Serilog;
using ShootingCompetitionsRequests.Models;
using ShootServ.Models;

namespace ShootServ.Controllers
{
    public class StandartFiltersController : BaseController
    {
        public StandartFiltersController(ILogger logger) : base(logger)
        {
        }
        
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
