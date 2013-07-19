using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderUpdateValidator : ValidatorBase<Order>
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
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 256, Errors.LENGTH_WRONG);

            this.Field(Target.ShipCity, "ShipCity")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(Target.ShipCountry, "ShipCountry")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(2, 256, Errors.LENGTH_WRONG);

            this.Field(Target.ShipZip, "ShipZip")
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(5, 5, Errors.LENGTH_WRONG);

            this.Field(Target.Total, "Total")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field(Target.OrderItems, "OrderItems")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);
        }
    }
}