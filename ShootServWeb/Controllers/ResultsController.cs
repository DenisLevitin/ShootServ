using System.Web.Mvc;
using BL;
using Serilog;

namespace ShootServ.Controllers
{
    public class ResultsController : BaseController
    {
        private readonly ResultLogic _resultsLogic;

        public ResultsController(ILogger logger) : base(logger)
        {
            _resultsLogic = new ResultLogic();
        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
