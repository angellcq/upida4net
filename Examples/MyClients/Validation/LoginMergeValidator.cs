using System;
using MyClients.Domain;

namespace MyClients.Validation
{
	public class LoginMergeValidator : HandyValidator<Login>
	{
		public override void Validate(object state)
		{
			self.Field("id", this.Target.Id);
			self.RequiredIfAssigned();

			self.Field("name", this.Target.Name);
			self.Required();
			self.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			self.Field("password", this.Target.Password);
			self.Required();
			self.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			self.Field("enabled", this.Target.Enabled);
			self.Required(Errors.NOT_VALID_BOOL);

			self.MissingField("client", this.Target.Client);
		}
	}
}