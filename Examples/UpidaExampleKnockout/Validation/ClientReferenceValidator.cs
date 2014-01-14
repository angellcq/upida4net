using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class ClientReferenceValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			self.Field("id", this.Target.Id);
			self.Required(Errors.MUST_BE_NUMBER);

			self.MissingField("name", Target.Name);
		}
	}
}