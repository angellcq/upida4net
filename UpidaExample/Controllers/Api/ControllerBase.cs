using System.Web.Http;
using Upida;
using Upida.Validation;

namespace UpidaExample.Controllers.Api
{
    public class ControllerBase : ApiController
    {
        protected JsonParser jsonParser;
        protected Validator validator;

        public ControllerBase(JsonParser jsonParser, Validator validator)
        {
            this.jsonParser = jsonParser;
            this.validator = validator;
        }
    }
}