using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class ClientSaveValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			self.MissingField("id", this.Target.Id);

			self.Field("name", this.Target.Name);
			self.Required();
			self.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);
			self.MustBeEmail(Errors.EMAIL);
		}
	}
}