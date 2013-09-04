using Upida.Validation;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Validation
{
    public class OrderItemSaveValidator : TypeValidatorBase<OrderItem>
    {
        public override void Validate()
        {
            this.Missing("Id", this.Target.Id);

            this.Required("Count", this.Target.Count);
            this.GreaterThan(0, Errors.GREATER_ZERO);

            this.Required("Price", this.Target.Price);
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Required("ProductId", this.Target.ProductId);

            this.Missing("Order", this.Target.Id);
        }
    }
}