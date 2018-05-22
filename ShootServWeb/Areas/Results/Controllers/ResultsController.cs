using BL;
using System.Web.Mvc;

namespace ShootServ.Areas.Results
{
    public class ResultsController : Controller
    {
        private readonly ResultLogic _resultsLogic;

        public ResultsController()
        {
            _resultsLogic = new ResultLogic();
        }
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
