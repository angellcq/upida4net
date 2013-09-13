using System.Web.Mvc;
using Upida;
using Upida.Validation;

namespace UpidaExampleStraight.Controllers
{
    public class ControllerBase : Controller
    {
        protected IFormParser formParser;
        protected IValidator validator;

        public ControllerBase(IFormParser formParser, IValidator validator)
        {
            this.formParser = formParser;
            this.validator = validator;
        }
    }
}