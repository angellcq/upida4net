using System;
using MyClients.Domain;

namespace MyClients.Validation
{
    public class LoginMergeValidator : HandyValidator<Login>
    {
        public override void Validate(object state)
        {
            this.ValidFormatOrNull("Id", this.Target.Id);

            this.Required("Name", this.Target.Name);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("password", this.Target.Password);
            this.Length(3, 20, Errors.LENGTH_3_20);

            this.Required("Enabled", this.Target.Enabled);

            this.Missing("client", this.Target.Client);
        }
    }
}