﻿using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class OrderItemSaveOrUpdateValidator : ValidatorBase<OrderItem>
    {
        public override void Validate()
        {
            if (this.Target.isFieldAssigned("Id"))
            {
                // Validate for update
                this.Field("Id", this.Target.Id);
                this.RequiredNumber();
            }

            this.Field("Count", this.Target.Count);
            this.RequiredNumber();
            this.GreaterThan(0, Errors.GREATER_ZERO);

            this.Field("Price", this.Target.Price);
            this.RequiredNumber();
            this.GreaterThan(0f, Errors.GREATER_ZERO);

            this.Field("ProductId", this.Target.ProductId);
            this.RequiredNumber();

            this.MissingField("Order");
        }
    }
}