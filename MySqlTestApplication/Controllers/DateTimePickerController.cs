using System.Web.Mvc;

namespace MySqlTestApplication.Controllers
{
    public class DateTimePickerController : Controller
    {
        [HttpPost]
        public ActionResult SearchByDateInBetween()
        {
            HomeController homeController = new HomeController();
            return ViewBag;
        }
    }
}