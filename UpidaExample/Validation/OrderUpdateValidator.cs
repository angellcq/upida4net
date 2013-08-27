﻿using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderUpdateValidator : TypeValidatorBase<Order>
    {
        public override void Validate()
        {
            this.Required("Id", Target.Id);

            this.Missing("CreatedOn", this.Target.CreatedOn);

            this.Required("ShipAddress", Target.ShipAddress);
            this.Length(5, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCity", Target.ShipCity);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipCountry", Target.ShipCountry);
            this.Length(2, 256, Errors.LENGTH_WRONG);

            this.Required("ShipZip", Target.ShipZip);
            this.Length(5, 5, Errors.LENGTH_WRONG);

            this.Required("Total", Target.Total);
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Missing("OrderItems", this.Target.OrderItems);
            this.Missing("Client", this.Target.Client);
        }
    }
}