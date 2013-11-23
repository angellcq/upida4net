using System;
using MyClients.Domain;

namespace MyClients.Validation
{
	public class LoginMergeValidator : HandyValidator<Login>
	{
		public override void Validate(object state)
		{
			this.Field("id", this.Target.Id);
			if (this.IsAssigned() && !this.IsNull())
			{
				this.Required(Errors.NOT_VALID_NUMBER);
			}

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