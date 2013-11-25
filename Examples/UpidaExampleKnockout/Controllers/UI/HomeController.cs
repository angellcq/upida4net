using System.Web.Mvc;

namespace UpidaExampleKnockout.Controllers.UI
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return this.RedirectToAction("list", "client");
		}
	}
}