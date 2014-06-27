using System.Web.Mvc;

namespace UpidaExampleAngularEF.Controllers.UI
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return this.View();
		}

		public ActionResult Notfound()
		{
			return this.View();
		}
	}
}