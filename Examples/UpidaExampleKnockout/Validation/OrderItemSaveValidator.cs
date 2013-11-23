using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderItemSaveValidator : HandyValidator<OrderItem>
	{
		public override void Validate(object state)
		{
			this.MissingField("Id", this.Target.Id);

			this.Field("Count", this.Target.Count);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0, Errors.GREATER_ZERO);

			this.Field("Price", this.Target.Price);
			this.Required(Errors.NOT_VALID_MONEY);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.Field("ProductId", this.Target.ProductId);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("Order", this.Target.Id);
		}
	}
}