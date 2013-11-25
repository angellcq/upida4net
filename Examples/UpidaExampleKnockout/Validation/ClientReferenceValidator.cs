using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class ClientReferenceValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			this.Field("id", this.Target.Id);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("name", Target.Name);
		}
	}
}