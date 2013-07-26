using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderUpdateItemsValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field("Id", Target.Id);
            this.RequiredNumber();

            this.MissingField("CreatedOn");
            this.MissingField("ShipAddress");
            this.MissingField("ShipCity");
            this.MissingField("ShipCountry");
            this.MissingField("ShipZip");
            this.MissingField("Total");

            this.Field("OrderItems", Target.OrderItems);
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.SAVE_OR_UPDATE);

            this.MissingField("Client");
        }
    }
}