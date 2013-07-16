using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderSaveValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field(this.target.Id, "Id")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(this.target.CreatedOn, "CreatedOn")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(this.target.ShipAddress, "ShipAddress")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 256, Errors.LENGTH_WRONG);

            this.Field(this.target.ShipCity, "ShipCity")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(this.target.ShipCountry, "ShipCountry")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(this.target.ShipZip, "ShipZip")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 5, Errors.LENGTH_WRONG);

            this.Field(this.target.Total, "Total")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field(this.target.OrderItems, "OrderItems")
                .Size(1, 500, Errors.WRONG_COUNT)
                .NestedList<OrderItem>(Groups.SAVE);
        }
    }
}