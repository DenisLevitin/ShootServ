using System.Web.Mvc;
using ShootingCompetitionsRequests.App_Start;
using ShootingCompetitionsRequests.Controllers;
using ShootServ.Areas.ShootingRange.Models;

namespace ShootServ.Areas.ShootingRange.Controllers
{
    public class ShootingRangeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ShootingRangeModelParams
            {
                IsLogin = CurrentUser != null
            };
            
            // Определяем залогирован ли пользователь в системе
            return View("Index", model);
        }

        [HttpGet]
        public ActionResult GetListByRegion(int? idRegion)
        {
            var model = ShootingRangeModelLogic.GetAllByRegion(idRegion);
            return PartialView("ListShootingRanges", model);
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Add(ShootingRangeModelParams model)
        {
            var res = ShootingRangeModelLogic.Add(model, CurrentUser.Id);
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Delete(int idShootingRange)
        {
            var res = ShootingRangeModelLogic.Delete(idShootingRange, CurrentUser.Id);
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
