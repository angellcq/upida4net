using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderSaveValidator : TypeValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Missing("Id", this.Target.Id);

            this.Required("ShipAddress", this.Target.ShipAddress);
            this.Length(5, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCity", this.Target.ShipCity);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCountry", this.Target.ShipCountry);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipZip", this.Target.ShipZip);
            this.Length(5, 5, Errors.LENGTH_WRONG);

            this.Required("Total", this.Target.Total);
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Required("OrderItems", this.Target.OrderItems);
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.SAVE);

            this.Required("Client", this.Target.Client);
        }
    }
}