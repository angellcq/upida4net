﻿using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderUpdateValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.Field("id", Target.Id);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("createdOn", this.Target.CreatedOn);

			this.Field("shipAddress", Target.ShipAddress);
			this.Required();
			this.MustHaveLengthBetween(5, 256, Errors.LENGTH_WRONG);

			this.Field("shipCity", Target.ShipCity);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("shipCountry", Target.ShipCountry);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("shipZip", Target.ShipZip);
			this.Required();
			this.MustHaveLengthBetween(5, 5, Errors.LENGTH_WRONG);

			this.Field("total", Target.Total);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.MissingField("orderItems", this.Target.OrderItems);
			this.MissingField("client", this.Target.Client);
		}
	}
}