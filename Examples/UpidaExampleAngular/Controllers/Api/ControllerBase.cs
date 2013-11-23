using System.Web.Http;
using Upida.Validation;

namespace UpidaExampleAngular.Controllers.Api
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