using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation
{
	public class ClientSaveValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			self.MissingField("id", this.Target.Id);

			self.Field("name", this.Target.Name);
			self.Required();
			self.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			self.Field("lastname", this.Target.Lastname);
			self.Required();
			self.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			self.Field("age", this.Target.Age);
			self.Required(Errors.MUST_BE_NUMBER);
			self.MustBeGreaterThan(0, Errors.GREATER_ZERO);

			self.Field("logins", this.Target.Logins);
			self.Required();
			self.MustHaveCountBetween(1, 5, Errors.NUMBER_OF_LOGINS);
			self.Stop();
			self.NestedList<Login>(Groups.SAVE, null);
		}
	}
}