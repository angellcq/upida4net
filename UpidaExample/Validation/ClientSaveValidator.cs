using Upida.Validation;
using UpidaExample.Domain;

namespace UpidaExample.Validation
{
    public class ClientSaveValidator : TypeValidatorBase<Client>
    {
        public override void Validate()
        {
            this.MissingField("Id");

            this.Field("Name", Target.Name);
            this.Required();
            this.Length(1, 256, Errors.LENGTH_WRONG);
            this.Email(Errors.NOT_EMAIL);
        }
    }
}