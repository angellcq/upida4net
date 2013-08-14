﻿using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderItemSaveOrUpdateValidator : ValidatorBase<OrderItem>
    {
        public override void Validate()
        {
            this.Field("Id", this.Target.Id);
            this.ValidFormatOrNotAssigned();

            this.Field("Count", this.Target.Count);
            this.Required();
            this.GreaterThan(0, Errors.GREATER_ZERO);

            this.Field("Price", this.Target.Price);
            this.Required();
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field("ProductId", this.Target.ProductId);
            this.Required();

            this.MissingField("Order");
        }
    }
}