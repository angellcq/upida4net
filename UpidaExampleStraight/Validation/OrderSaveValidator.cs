using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
{
    public class OrderSaveValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.MissingField("Id");
            this.MissingField("CreatedOn");

            this.Field("ShipAddress", this.Target.ShipAddress);
            this.Required();
            this.Length(5, 256, Errors.LENGTH_WRONG);

            this.Field("ShipCity", this.Target.ShipCity);
            this.Required();
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Field("ShipCountry", this.Target.ShipCountry);
            this.Required();
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Field("ShipZip", this.Target.ShipZip);
            this.Required();
            this.Length(5, 5, Errors.LENGTH_WRONG);

            this.Field("Total", this.Target.Total);
            this.Required();
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field("OrderItems", this.Target.OrderItems);
            this.Required();
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.SAVE);

            this.Field("Client", this.Target.Client);
            this.Required();
        }
    }
}