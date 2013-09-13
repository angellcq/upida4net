using System.Collections.Generic;
using Upida.Validation;
using UpidaExampleKnockout.Business;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Controllers.Api
{
    public class ClientController : ControllerBase
    {
        private readonly ClientBusiness clientBusiness;

        public ClientController(IValidator validator, ClientBusiness clientBusiness)
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