using System.Web.Mvc;

namespace UpidaExampleAngular.Controllers.UI
{
	public class ClientController : Controller
	{
		public ActionResult List()
		{
			return this.View();
		}

		public ActionResult Create()
		{
			return this.View();
		}
	}
}