using System.Web.Mvc;
using ShootServ.Helpers;
using ShootServ.Models.ShootingRange;

namespace ShootServ.Controllers
{
    public class ShootingRangeController : BaseController
    {
        public ActionResult Index()
        {
            var model = new ShootingRangeModelParams();            
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
            if (ModelState.IsValid)
            {
                var res = ShootingRangeModelLogic.Add(model, CurrentUser.Id);
                return new JsonResult {Data = new {res.IsOk, Message = res.ErrorMessage}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
            }

            return new JsonResult { Data = new { IsOk = false, Message = string.Empty, ValidationMessages = ModelState.ToErrorsDictionary()}, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Delete(int idShootingRange)
        {
            var res = ShootingRangeModelLogic.Delete(idShootingRange, CurrentUser.Id);
            return new JsonResult { Data = new {res.IsOk, Message = res.ErrorMessage }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
