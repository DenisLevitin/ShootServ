﻿using System.Web.Http;
using BL;

namespace ShootServ.Controllers.API
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
