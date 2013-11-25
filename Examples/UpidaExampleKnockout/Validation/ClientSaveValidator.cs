using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class ClientSaveValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			this.MissingField("id", this.Target.Id);

			this.Field("name", Target.Name);
			this.Required();
			this.MustHaveLengthBetween(1, 256, Errors.LENGTH_WRONG);
			this.MustBeEmail(Errors.NOT_EMAIL);
		}
	}
}