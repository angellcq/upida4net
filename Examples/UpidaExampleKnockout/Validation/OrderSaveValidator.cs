using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderSaveValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.MissingField("Id", this.Target.Id);

			this.Field("ShipAddress", this.Target.ShipAddress);
			this.Required();
			this.MustHaveLengthBetween(5, 256, Errors.LENGTH_WRONG);

			this.Field("ShipCity", this.Target.ShipCity);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("ShipCountry", this.Target.ShipCountry);
			this.Required();
			this.MustHaveLengthBetween(2, 256, Errors.LENGTH_WRONG);

			this.Field("ShipZip", this.Target.ShipZip);
			this.Required();
			this.MustHaveLengthBetween(5, 5, Errors.LENGTH_WRONG);

			this.Field("Total", this.Target.Total);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.Field("OrderItems", this.Target.OrderItems);
			this.Required();
			this.MustHaveSizeBetween(1, 500, Errors.WRONG_COUNT);
			this.NestedList<OrderItem>(Groups.SAVE, null);

			this.Field("Client", this.Target.Client);
			this.Required();
			this.Nested<Client>(Groups.REFERENCE, null);
		}
	}
}