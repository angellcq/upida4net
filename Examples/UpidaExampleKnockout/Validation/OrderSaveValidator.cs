using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderSaveValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.MissingField("id", this.Target.Id);

			this.Field("shipAddress", this.Target.ShipAddress);
			this.Required();
			this.MustHaveLengthBetween(5, 50, Errors.LENGTH_5_AND_50);

			this.Field("shipCity", this.Target.ShipCity);
			this.Required();
			this.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			this.Field("shipCountry", this.Target.ShipCountry);
			this.Required();
			this.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			this.Field("shipZip", this.Target.ShipZip);
			this.Required();
			this.MustHaveLengthBetween(5, 5, Errors.LENGTH_ZIP);

			this.Field("total", this.Target.Total);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.Field("orderItems", this.Target.OrderItems);
			this.Required();
			this.MustHaveSizeBetween(1, 500, Errors.WRONG_COUNT);
			this.NestedList<OrderItem>(Groups.SAVE, null);

			this.Field("client", this.Target.Client);
			this.Required();
			this.Nested<Client>(Groups.REFERENCE, null);
		}
	}
}