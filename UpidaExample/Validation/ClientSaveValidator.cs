using System;
using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class ClientSaveValidator : ValidatorBase<Client>
    {
        public override void Validate()
        {
            this.Field(target.Id, "Id")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(target.Name, "Name")
                .MustBeAssigned(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(1, 256, Errors.LENGTH_WRONG);
        }
    }
}