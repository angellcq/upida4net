using System.Collections.Generic;
using System.Web.Http;
using Upida.Validation;
using UpidaExampleAngularEF.Business;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Controllers.Api
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

		[HttpPost]
		public void Delete(int id)
		{
			this.clientBusiness.Delete(id);
		}
	}
}