using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderUpdateItemsValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			this.Field("Id", this.Target.Id);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("CreatedOn", this.Target.CreatedOn);
			this.MissingField("ShipAddress", this.Target.ShipAddress);
			this.MissingField("ShipCity", this.Target.ShipCity);
			this.MissingField("ShipCountry", this.Target.ShipCountry);
			this.MissingField("ShipZip", this.Target.ShipZip);
			this.MissingField("Total", this.Target.Total);

			this.Field("OrderItems", this.Target.OrderItems);
			this.Required();
			this.MustHaveSizeBetween(1, 500, Errors.WRONG_COUNT);
			this.NestedList<OrderItem>(Groups.MERGE, null);

			this.MissingField("Client", this.Target.Client);
		}
	}
}