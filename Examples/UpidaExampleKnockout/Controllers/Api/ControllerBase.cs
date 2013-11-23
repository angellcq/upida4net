using System.Web.Http;
using Upida.Validation;

namespace UpidaExampleKnockout.Controllers.Api
{
    public class ControllerBase : ApiController
    {
        protected IValidationContext validator;

        public ControllerBase(IValidationContext validator)
        {
            this.validator = validator;
        }
    }
}