using Upida.Validation;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Validation
{
	public class OrderUpdateValidator : HandyValidator<Order>
	{
		public override void Validate(object state)
		{
			self.Field("id", this.Target.Id);
			self.SetSeverity(Severity.Fatal);
			self.Required(Errors.MUST_BE_NUMBER);

			self.MissingField("createdOn", this.Target.CreatedOn);

			self.Field("shipAddress", this.Target.ShipAddress);
			self.Required();
			self.MustHaveLengthBetween(5, 50, Errors.LENGTH_5_AND_50);

			self.Field("shipCity", this.Target.ShipCity);
			self.Required();
			self.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			self.Field("shipCountry", this.Target.ShipCountry);
			self.Required();
			self.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50);

			self.Field("shipZip", this.Target.ShipZip);
			self.Required();
			self.MustHaveLengthBetween(5, 5, Errors.LENGTH_ZIP);

			self.Field("total", this.Target.Total);
			self.Required(Errors.MUST_BE_NUMBER);
			self.MustBeGreaterThan(0f, Errors.GREATER_ZERO);

			self.MissingField("orderItems", this.Target.OrderItems);
			self.MissingField("client", this.Target.Client);
		}
	}
}