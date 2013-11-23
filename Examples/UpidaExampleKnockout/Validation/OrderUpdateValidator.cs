using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderUpdateValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.Field("Id", Target.Id);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("CreatedOn", this.Target.CreatedOn);

			this.Field("ShipAddress", Target.ShipAddress);
			this.Required();
			this.MustHaveLengthBetween(5, 256, Errors.LENGTH_WRONG);

			this.Field("ShipCity", Target.ShipCity);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("ShipCountry", Target.ShipCountry);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("ShipZip", Target.ShipZip);
			this.Required();
			this.MustHaveLengthBetween(5, 5, Errors.LENGTH_WRONG);

			this.Field("Total", Target.Total);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.MissingField("OrderItems", this.Target.OrderItems);
			this.MissingField("Client", this.Target.Client);
		}
	}
}