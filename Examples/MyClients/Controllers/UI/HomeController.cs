using System.Web.Mvc;

namespace MyClients.Controllers.UI
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