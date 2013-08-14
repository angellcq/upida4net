using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
{
    public class OrderUpdateValidator : ValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Field("Id", Target.Id);
            this.Required();

            this.MissingField("CreatedOn");

            this.Field("ShipAddress", Target.ShipAddress);
            this.Required();
            this.Length(5, 256, Errors.LENGTH_WRONG);

            this.Field("ShipCity", Target.ShipCity);
            this.Required();
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Field("ShipCountry", Target.ShipCountry);
            this.Required();
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Field("ShipZip", Target.ShipZip);
            this.Required();
            this.Length(5, 5, Errors.LENGTH_WRONG);

            this.Field("Total", Target.Total);
            this.Required();
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.MissingField("OrderItems");
            this.MissingField("Client");
        }
    }
}