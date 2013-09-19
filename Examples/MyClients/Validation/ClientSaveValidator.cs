using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation
{
    public class ClientSaveValidator : HandyValidator<Client>
    {
        public override void Validate(object state)
        {
            this.Missing("Id", this.Target.Id);

            this.Required("Name", this.Target.Name);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("Lastname", this.Target.Lastname);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("Age", this.Target.Age);

            this.Required("Logins", this.Target.Logins);
            this.Size(1, 5, Errors.NUMBER_OF_LOGINS);
            this.Stop();
            this.NestedList<Login>(Groups.SAVE, null);
        }
    }
}