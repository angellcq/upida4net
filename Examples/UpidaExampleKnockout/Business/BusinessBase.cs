using Upida;
using Upida.Validation;

namespace UpidaExampleKnockout.Business
{
	public class BusinessBase
	{
		protected IMapper mapper;
		protected IValidationContext validator;

		public BusinessBase(IMapper mapper, IValidationContext validator)
		{
			this.mapper = mapper;
			this.validator = validator;
		}
	}
}