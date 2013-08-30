using System.Web.Http;
using Upida.Validation;

namespace UpidaExample.Controllers.Api
{
    public class ControllerBase : ApiController
    {
        protected IValidator validator;

        public ControllerBase(IValidator validator)
        {
            this.validator = validator;
        }
    }
}