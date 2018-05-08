using System.Web.Mvc;
using BO;

namespace ShootingCompetitionsRequests.Controllers
{
    public class BaseController : Controller
    {
        public UserParams CurrentUser
        {
            get { return Session["user"] as UserParams; }
        }
    }
}