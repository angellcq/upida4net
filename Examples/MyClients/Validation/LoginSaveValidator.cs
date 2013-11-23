using System;
using MyClients.Domain;

namespace MyClients.Validation
{
	public class LoginSaveValidator : HandyValidator<Login>
	{
		public override void Validate(object state)
		{
			this.MissingField("id", this.Target.Id);

			this.Field("name", this.Target.Name);
			this.Required();
			this.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			this.Field("password", this.Target.Password);
			this.Required();
			this.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			this.Field("enabled", this.Target.Enabled);
			this.Required(Errors.NOT_VALID_BOOL);

			this.MissingField("client", this.Target.Client);
		}
	}
}