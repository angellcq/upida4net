using System.Web.Mvc;

namespace UpidaExampleStraight.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return this.RedirectToAction("index", "client");
        }
    }
}