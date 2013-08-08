using System.Web.Mvc;
using Upida;
using Upida.Validation;

namespace UpidaExampleStraight.Controllers
{
    public class ControllerBase : Controller
    {
        protected FormParser formParser;
        protected Validator validator;

        public ControllerBase(FormParser formParser, Validator validator)
        {
            this.formParser = formParser;
            this.validator = validator;
        }
    }
}