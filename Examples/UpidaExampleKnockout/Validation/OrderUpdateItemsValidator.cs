﻿using Upida.Validation;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Validation
{
    public class OrderUpdateItemsValidator : HandyValidator<Order>
    {
        public override void Validate(object state)
        {
            this.Required("Id", this.Target.Id);

            this.Missing("CreatedOn", this.Target.CreatedOn);
            this.Missing("ShipAddress", this.Target.ShipAddress);
            this.Missing("ShipCity", this.Target.ShipCity);
            this.Missing("ShipCountry", this.Target.ShipCountry);
            this.Missing("ShipZip", this.Target.ShipZip);
            this.Missing("Total", this.Target.Total);

            this.Required("OrderItems", this.Target.OrderItems);
            this.Size(1, 500, Errors.WRONG_COUNT);
            this.NestedList<OrderItem>(Groups.MERGE, null);

            this.Missing("Client", this.Target.Client);
        }
    }
}