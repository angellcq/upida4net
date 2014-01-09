using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class ClientReferenceValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			this.Field("id", this.Target.Id);
			this.Required(Errors.MUST_BE_NUMBER);

			this.MissingField("name", Target.Name);
		}
	}
}