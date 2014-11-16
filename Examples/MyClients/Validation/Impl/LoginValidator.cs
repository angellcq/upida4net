using System;
using System.Collections.Generic;
using MyClients.Domain;

namespace MyClients.Validation.Impl
{
    public class LoginValidator : ILoginValidator
    {
        private IHandyValidatorFactory validatorFactory;

        public void ValidateForSave(IEnumerable<Login> logins, IHandyValidator parentContext)
        {
            if (null == logins) return;

            int index = 0;
            IHandyValidator context = this.validatorFactory.Get(parentContext);
            foreach (Login target in logins)
            {
                context.SetIndex(index++);
                context.MissingField("id", target.Id);

                context.SetField("name", target.Name);
                context.Required();
                context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                context.SetField("password", target.Password);
                context.Required();
                context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                context.SetField("enabled", target.Enabled);
                context.Required(Errors.NOT_VALID_BOOL);

                context.MissingField("client", target.Client);
            }
        }

        public void ValidateForMerge(IEnumerable<Login> logins, IHandyValidator parentContext)
        {
            if (null == logins) return;

            int index = 0;
            IHandyValidator context = this.validatorFactory.Get(parentContext);
            foreach (Login target in logins)
            {
                context.SetIndex(index++);
                context.SetField("id", target.Id);
                context.RequiredIfAssigned();

                context.SetField("name", target.Name);
                context.Required();
                context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                context.SetField("password", target.Password);
                context.Required();
                context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                context.SetField("enabled", target.Enabled);
                context.Required(Errors.NOT_VALID_BOOL);

                context.MissingField("client", target.Client);
            }
        }
    }
}