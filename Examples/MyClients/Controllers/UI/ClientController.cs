using System.Web.Mvc;

namespace MyClients.Controllers.UI
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

        public ActionResult Edit()
        {
            return this.View();
        }
    }
}