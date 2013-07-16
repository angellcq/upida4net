using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExample.Business;
using UpidaExample.Domain;

namespace UpidaExample.Controllers.Api
{
    public class ClientController : ControllerBase
    {
        private readonly ClientBusiness clientBusiness;

        public ClientController(JsonParser jsonParser, Validator validator, ClientBusiness clientBusiness)
            : base(jsonParser, validator)
        {
            this.clientBusiness = clientBusiness;
        }

        public IList<Client> GetAll()
        {
            return this.clientBusiness.GetAll();
        }

        public void Save(JToken item)
        {
            Client data = this.jsonParser.Parse<Client>(item);
            this.validator.Validate(data, Groups.SAVE);
            this.clientBusiness.Save(data);
        }
    }
}