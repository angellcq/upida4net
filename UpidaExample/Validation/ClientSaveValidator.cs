using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class ClientSaveValidator : TypeValidatorBase<Client>
    {
        public override void Validate()
        {
            this.Missing("Id", this.Target.Id);

            this.Required("Name", Target.Name);
            this.Length(1, 256, Errors.LENGTH_WRONG);
            this.Email(Errors.NOT_EMAIL);
        }
    }
}