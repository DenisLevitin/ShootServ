using System.Web.Mvc;
using BL;
using BO;
using ShootServ.Helpers;
using ShootServ.Models.ShootingRange;

namespace ShootServ.Controllers
{
    public class ShootingRangeController : BaseController
    {
        private readonly ShootingRangeLogic _shootingRangeLogic;

        public ShootingRangeController()
        {
            _shootingRangeLogic = new ShootingRangeLogic();
        }
        
        public ActionResult Index()
        {
            var model = new ShootingRangeModelParams();            
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult GetListByRegion(int? idRegion)
        {
            var model = _shootingRangeLogic.GetByRegion(idRegion);
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Add(ShootingRangeModelParams model)
        {
            if (ModelState.IsValid)
            {
                var shootingRange = new ShootingRangeParams
                {
                    Id = model.Id,
                    IdRegion = model.RegionId,
                    Address = model.Address,
                    Name = model.Name,
                    Info = model.Info,
                    IdUser = CurrentUser.Id,
                    Phone = model.Phone,
                    Town = model.Town
                };
                
                var res = _shootingRangeLogic.Add(shootingRange, CurrentUser.Id);
                return new JsonResult {Data = new {res.IsOk, Message = res.ErrorMessage}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }

            return new JsonResult { Data = new { IsOk = false, Message = string.Empty, ValidationMessages = ModelState.ToErrorsDictionary()}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult Delete(int idShootingRange)
        {
            var res = _shootingRangeLogic.Delete(idShootingRange, CurrentUser.Id);
            return new JsonResult { Data = new { res.IsOk, Message = res.ErrorMessage } };
        }
    }
}
