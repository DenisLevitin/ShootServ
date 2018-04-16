using BO;
using ShootingCompetitionsRequests.App_Start;
using ShootingCompetitionsRequests.Areas.ShootingRange.Models;
using System.Web.Mvc;

namespace ShootingCompetitionsRequests.Areas.ShootingRange.Controllers
{
    public class AddShootingRangeController : Controller
    {
        //
        // GET: /ShootingRange/AddShootingRange/

        //[CustomAuthorize]
        public ActionResult Index()
        {
            var model = new ShootingRangeModelParams
            {
                IsLogin = Session["user"] != null
            };
            // Определяем залогирован ли пользователь в системе

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult GetListByRegion(int idRegion)
        {
            var model = ShootingRangeModelLogic.GetAllByRegion(idRegion);
            return PartialView("ListShootingRanges", model);
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Add(ShootingRangeModelParams model)
        {
            var res = ShootingRangeModelLogic.Add(model, _user.Id);
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Delete(int idShootingRange)
        {
            var res = ShootingRangeModelLogic.Delete(idShootingRange, _user.Id);
            return new JsonResult { Data = new { IsOk = res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private UserParams _user
        {
            get
            {
                return Session["user"] as UserParams;
            }
        }

    }
}
