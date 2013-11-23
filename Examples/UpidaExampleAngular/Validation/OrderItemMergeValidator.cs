using Upida.Validation;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Validation
{
	public class OrderItemMergeValidator : HandyValidator<OrderItem>
	{
		public override void Validate(object state)
		{
			this.Field("id", this.Target.Id);
			if (this.IsAssigned() && !this.IsNull())
			{
				this.Required(Errors.NOT_VALID_NUMBER);
			}

			this.Field("count", this.Target.Count);
			this.Required(Errors.NOT_VALID_NUMBER);
			this.MustBeGreaterThan(0, Errors.GREATER_ZERO);

			this.Field("price", this.Target.Price);
			this.Required(Errors.NOT_VALID_MONEY);
			this.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			this.Field("productId", this.Target.ProductId);
			this.Required(Errors.NOT_VALID_NUMBER);

			this.MissingField("order", this.Target.Order);
		}
	}
}