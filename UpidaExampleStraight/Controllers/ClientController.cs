using System.Collections.Generic;
using System.Web.Mvc;
using Upida;
using Upida.Validation;
using UpidaExampleStraight.Business;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly ClientBusiness clientBusiness;

        public ClientController(IFormParser formParser, IValidator validator, ClientBusiness clientBusiness)
            : base(formParser, validator)
        {
            this.clientBusiness = clientBusiness;
        }

        public ActionResult Index()
        {
            IList<Client> items = this.clientBusiness.GetAll();
            return View(items);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public ActionResult Save()
        {
            Client item = this.formParser.Parse<Client>(this.Request.Params);
            bool success = this.validator.ValidateAndPublish(item, Groups.SAVE, this.ModelState);
            if (!success)
            {
                return View(item);
            }

            this.clientBusiness.Save(item);
            return this.RedirectToAction("index");
        }
    }
}