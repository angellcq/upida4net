using System.Web.Mvc;

namespace UpidaExampleAngular.Controllers.UI
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {
            return this.RedirectToAction("index", "client");
        }
    }
}