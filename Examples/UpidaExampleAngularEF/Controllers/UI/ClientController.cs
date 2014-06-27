using System.Web.Mvc;

namespace UpidaExampleAngularEF.Controllers.UI
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