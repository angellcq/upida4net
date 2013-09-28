using System;
using MyClients.Domain;

namespace MyClients.Validation
{
    public class LoginSaveValidator : HandyValidator<Login>
    {
        public override void Validate(object state)
        {
            this.Missing("Id", this.Target.Id);

            this.Required("Name", this.Target.Name);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("Password", this.Target.Password);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("Enabled", this.Target.Enabled);

            this.Missing("Client", this.Target.Client);
        }
    }
}