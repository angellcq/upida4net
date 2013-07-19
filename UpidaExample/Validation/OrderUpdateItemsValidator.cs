using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderUpdateItemsValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field(Target.Id, "Id")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY);

            this.Field(Target.CreatedOn, "CreatedOn")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.ShipAddress, "ShipAddress")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.ShipCity, "ShipCity")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.ShipCountry, "ShipCountry")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.ShipZip, "ShipZip")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.Total, "Total")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.OrderItems, "OrderItems")
                .Size(1, 500, Errors.WRONG_COUNT)
                .NestedList<OrderItem>(Groups.SAVE_OR_UPDATE);
        }
    }
}