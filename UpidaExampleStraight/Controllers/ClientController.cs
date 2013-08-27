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
            try
            {
                this.validator.AssertValid(item, Groups.SAVE);
                this.clientBusiness.Save(item);
                return View(item);
            }
            catch (ValidationException ex)
            {
                this.validator.PublishFailures(ex.GetFailures(), this.ModelState);
                return this.RedirectToAction("index");
            }
        }
    }
}