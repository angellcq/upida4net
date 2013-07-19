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
                this.Field(this.Target.Id, "Id")
                    .ValidFormat(Errors.INVALID_NUMBER);
            }

            this.Field(this.Target.Count, "Count")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0, Errors.GREATER_ZERO);
            this.Field(this.Target.Price, "Price")
                .ValidFormat(Errors.INVALID_NUMBER)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .GreaterThan(0f, Errors.GREATER_ZERO);
            this.Field(this.Target.ProductId, "ProductId")
                .NotNull(Errors.CANNOT_BE_EMPTY);
            this.Field(this.Target.Order, "Order")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);
        }
    }
}