using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderUpdateItemsValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			self.Field("id", this.Target.Id);
			self.SetSeverity(Severity.Fatal);
			self.Required(Errors.MUST_BE_NUMBER);

			self.MissingField("createdOn", this.Target.CreatedOn);
			self.MissingField("shipAddress", this.Target.ShipAddress);
			self.MissingField("shipCity", this.Target.ShipCity);
			self.MissingField("shipCountry", this.Target.ShipCountry);
			self.MissingField("shipZip", this.Target.ShipZip);
			self.MissingField("total", this.Target.Total);

			self.Field("orderItems", this.Target.OrderItems);
			self.Required();
			self.MustHaveCountBetween(1, 500, Errors.WRONG_COUNT);
			self.NestedList<OrderItem>(Groups.MERGE, null);

			self.MissingField("client", this.Target.Client);
		}
	}
}