using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderSaveValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field(this.Target.Id, "Id")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(this.Target.CreatedOn, "CreatedOn")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(this.Target.ShipAddress, "ShipAddress")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 256, Errors.LENGTH_WRONG);

            this.Field(this.Target.ShipCity, "ShipCity")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(this.Target.ShipCountry, "ShipCountry")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(this.Target.ShipZip, "ShipZip")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 5, Errors.LENGTH_WRONG);

            this.Field(this.Target.Total, "Total")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field(this.Target.OrderItems, "OrderItems")
                .Size(1, 500, Errors.WRONG_COUNT)
                .NestedList<OrderItem>(Groups.SAVE);
        }
    }
}