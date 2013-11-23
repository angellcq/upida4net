﻿using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation
{
	public class ClientSaveValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			this.MissingField("id", this.Target.Id);

			this.Field("name", this.Target.Name);
			this.Required();
			this.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			this.Field("lastname", this.Target.Lastname);
			this.Required();
			this.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

			this.Field("age", this.Target.Age);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.Field("logins", this.Target.Logins);
			this.Required();
			this.MustHaveSizeBetween(1, 5, Errors.NUMBER_OF_LOGINS);
			this.Stop();
			this.NestedList<Login>(Groups.SAVE, null);
		}
	}
}