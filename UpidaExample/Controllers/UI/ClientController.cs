using System.Web.Mvc;
using Upida;
using Upida.Validation;

namespace UpidaExample.Controllers.UI
{
    public class ClientController : Controller
    {
        public ActionResult index()
        {
            return View();
        }

        public ActionResult create()
        {
            return View();
        }
    }
}