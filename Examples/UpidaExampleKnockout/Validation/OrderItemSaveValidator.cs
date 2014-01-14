using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
	public class OrderItemSaveValidator : HandyValidator<OrderItem>
	{
		public override void Validate(object state)
		{
			self.MissingField("id", this.Target.Id);

			self.Field("count", this.Target.Count);
			self.Required(Errors.MUST_BE_NUMBER);
			self.MustBeGreaterThan(0, Errors.GREATER_ZERO);

			self.Field("price", this.Target.Price);
			self.Required(Errors.MUST_BE_MONEY);
			self.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			self.Field("productId", this.Target.ProductId);
			self.Required(Errors.MUST_BE_NUMBER);

			self.MissingField("order", this.Target.Id);
		}
	}
}