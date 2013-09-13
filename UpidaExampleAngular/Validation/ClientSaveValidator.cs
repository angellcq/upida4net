using Upida.Validation;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Validation
{
    public class ClientSaveValidator : HandyValidator<Client>
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