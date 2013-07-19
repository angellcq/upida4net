using System;
using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class ClientSaveValidator : ValidatorBase<Client>
    {
        public override void Validate()
        {
            this.Field(Target.Id, "Id")
                .MustBeUnassigned(Errors.MUST_BE_EMPTY);

            this.Field(Target.Name, "Name")
                .MustBeAssigned(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .NotNull(Errors.CANNOT_BE_EMPTY)
                .Stop()
                .Length(1, 256, Errors.LENGTH_WRONG);
        }
    }
}