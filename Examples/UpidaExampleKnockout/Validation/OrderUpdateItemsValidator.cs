using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderUpdateItemsValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.Field("id", this.Target.Id);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("createdOn", this.Target.CreatedOn);
			this.MissingField("shipAddress", this.Target.ShipAddress);
			this.MissingField("shipCity", this.Target.ShipCity);
			this.MissingField("shipCountry", this.Target.ShipCountry);
			this.MissingField("shipZip", this.Target.ShipZip);
			this.MissingField("total", this.Target.Total);

			this.Field("orderItems", this.Target.OrderItems);
			this.Required();
			this.MustHaveSizeBetween(1, 500, Errors.WRONG_COUNT);
			this.NestedList<OrderItem>(Groups.MERGE, null);

			this.MissingField("client", this.Target.Client);
		}
	}
}