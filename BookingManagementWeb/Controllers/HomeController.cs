using System.Web.Mvc;

namespace BookingManagementWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Client()
        {
            return View();
        }

        [Authorize]
        public ActionResult Staff()
        {
            return View();
        }

        [Authorize]
        public ActionResult ConsultationType()
        {
            return View();
        }

        [Authorize]
        public ActionResult StaffSession()
        {
            return View();
        }

        [Authorize]
        public ActionResult SessionsBooked()
        {
            return View();
        }
    }
}