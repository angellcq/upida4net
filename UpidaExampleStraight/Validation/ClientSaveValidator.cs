using Upida.Validation;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Validation
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