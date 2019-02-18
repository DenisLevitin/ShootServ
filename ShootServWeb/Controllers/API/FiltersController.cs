using BL;
using System.Web.Http;

namespace ShootServ.API.Controllers
{
    public class FiltersController : ApiController
    {
        private readonly RegionsLogic _regionsLogic; 

        public FiltersController()
        {
            _regionsLogic = new RegionsLogic();
        }

        [HttpGet]
        [Route("api/GetRegions/{idCountry}", Name = "GetRegions")]
        public object GetRegions([FromUri]int? idCountry)
        {
            var result = _regionsLogic.GetByCountry(idCountry);
            return Json(result);
        }
    }
}
