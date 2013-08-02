using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
{
    public class OrderItemSaveOrUpdateValidator : ValidatorBase<OrderItem>
    {
        public override void Validate()
        {
            this.Field("Id", this.Target.Id);
            this.ValidNumberOrNull();

            this.Field("Count", this.Target.Count);
            this.RequiredNumber();
            this.GreaterThan(0, Errors.GREATER_ZERO);

            this.Field("Price", this.Target.Price);
            this.RequiredNumber();
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field("ProductId", this.Target.ProductId);
            this.RequiredNumber();

            this.MissingField("Order");
        }
    }
}