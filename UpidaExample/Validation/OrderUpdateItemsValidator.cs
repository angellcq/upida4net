using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderUpdateItemsValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field(target.Id, "Id")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY);

            this.Field(target.CreatedOn, "CreatedOn")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.ShipAddress, "ShipAddress")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.ShipCity, "ShipCity")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.ShipCountry, "ShipCountry")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.ShipZip, "ShipZip")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.Total, "Total")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.OrderItems, "OrderItems")
                .Size(1, 500, Errors.WRONG_COUNT)
                .NestedList<OrderItem>(Groups.SAVE_OR_UPDATE);
        }
    }
}