﻿using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
{
    public class OrderSaveValidator : HandyValidator<Order>
    {
        public override void Validate(object state)
        {
            this.Missing("Id", this.Target.Id);

            this.Required("ShipAddress", this.Target.ShipAddress);
            this.Length(5, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCity", this.Target.ShipCity);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCountry", this.Target.ShipCountry);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipZip", this.Target.ShipZip);
            this.Length(5, 5, Errors.LENGTH_WRONG);

            this.Required("Total", this.Target.Total);
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Required("OrderItems", this.Target.OrderItems);
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.SAVE, null);

            this.Required("Client", this.Target.Client);
        }
    }
}