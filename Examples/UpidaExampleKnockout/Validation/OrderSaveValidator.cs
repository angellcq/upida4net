using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderSaveValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			self.MissingField("id", this.Target.Id);

			self.Field("shipAddress", this.Target.ShipAddress);
			self.Required();
			self.MustHaveLengthBetween(5, 50, Errors.LENGTH_5_AND_50);

			self.Field("shipCity", this.Target.ShipCity);
			self.Required();
			self.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			self.Field("shipCountry", this.Target.ShipCountry);
			self.Required();
			self.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			self.Field("shipZip", this.Target.ShipZip);
			self.Required();
			self.MustHaveLengthBetween(5, 5, Errors.LENGTH_ZIP);

			self.Field("total", this.Target.Total);
			self.Required(Errors.MUST_BE_NUMBER);
			self.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			self.Field("orderItems", this.Target.OrderItems);
			self.Required();
			self.MustHaveCountBetween(1, 500, Errors.WRONG_COUNT);
			self.NestedList<OrderItem>(Groups.SAVE, null);

			self.Field("client", this.Target.Client);
			self.Required();
			self.Nested<Client>(Groups.REFERENCE, null);
		}
	}
}