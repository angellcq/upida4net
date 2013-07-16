using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderItemSaveValidator : ValidatorBase<OrderItem>
    {
        public override void Validate()
        {
            this.Field(this.target.Id, "Id")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(this.target.Count, "Count")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0, Errors.GREATER_ZERO);
            this.Field(this.target.Price, "Price")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0f, Errors.GREATER_ZERO);
            this.Field(this.target.ProductId, "ProductId")
                .NotNull(Errors.CANNOT_BE_EMPTY);
            this.Field(this.target.Order, "Order")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);
        }
    }
}