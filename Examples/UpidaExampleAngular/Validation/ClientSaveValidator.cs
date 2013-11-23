﻿using Upida.Validation;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Validation
{
	public class ClientSaveValidator : HandyValidator<Client>
	{
		public override void Validate(object state)
		{
			this.MissingField("id", this.Target.Id);

			this.Field("name", Target.Name);
			this.Required();
			this.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);
			this.MustBeEmail(Errors.NOT_EMAIL);
		}
	}
}