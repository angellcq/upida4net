using System.Collections.Generic;
using Upida.Validation;
using UpidaExampleAngular.Business;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Controllers.Api
{
    public class ClientController : ControllerBase
    {
        private readonly ClientBusiness clientBusiness;

        public ClientController(IValidationContext validator, ClientBusiness clientBusiness)
            : base(validator)
        {
            this.clientBusiness = clientBusiness;
        }

        public IList<Client> GetAll()
        {
            return this.clientBusiness.GetAll();
        }

        public void Save(Client item)
        {
            this.validator.AssertValid(item, Groups.SAVE);
            this.clientBusiness.Save(item);
        }
    }
}