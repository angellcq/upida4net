using Upida;

namespace UpidaExampleKnockout.Business
{
	public class BusinessBase
	{
		protected IMapper mapper;

		public BusinessBase(IMapper mapper)
		{
			this.mapper = mapper;
		}
	}
}