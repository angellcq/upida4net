using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
{
    public class OrderUpdateItemsValidator : TypeValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field("Id", Target.Id);
            this.Required();

            this.MissingField("CreatedOn");
            this.MissingField("ShipAddress");
            this.MissingField("ShipCity");
            this.MissingField("ShipCountry");
            this.MissingField("ShipZip");
            this.MissingField("Total");

            this.Field("OrderItems", Target.OrderItems);
            this.Required();
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.MERGE);

            this.MissingField("Client");
        }
    }
}