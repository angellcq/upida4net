using System.Web.Http;
using Upida;
using Upida.Validation;

namespace UpidaExample.Controllers.Api
{
    public class ControllerBase : ApiController
    {
        protected IJsonParser jsonParser;
        protected IValidator validator;

        public ControllerBase(IJsonParser jsonParser, IValidator validator)
        {
            this.jsonParser = jsonParser;
            this.validator = validator;
        }
    }
}