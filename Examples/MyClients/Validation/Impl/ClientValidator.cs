using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation.Impl
{
    public class ClientValidator : IClientValidator
    {
        private IHandyValidatorFactory validatorFactory;
        private ILoginValidator loginvalidator;

        public void ValidateForSave(Client target)
        {
            if (null == target) return;
            IHandyValidator context = this.validatorFactory.Get(null);

            context.MissingField("id", target.Id);

            context.SetField("name", target.Name);
            context.Required();
            context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

            context.SetField("lastname", target.Lastname);
            context.Required();
            context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

            context.SetField("age", target.Age);
            context.Required(Errors.MUST_BE_NUMBER);
            context.MustBeGreaterThan(0, Errors.GREATER_ZERO);

            context.SetField("logins", target.Logins);
            context.Required();
            context.MustHaveCountBetween(1, 5, Errors.NUMBER_OF_LOGINS);

            context.SetNested();
            this.loginvalidator.ValidateForSave(target.Logins, context);

            context.Assert();
        }

        public void ValidateForUpdate(Client target)
        {
            if (null == target) return;
            IHandyValidator context = this.validatorFactory.Get(null);

            context.SetField("id", target.Id);
            context.Required();

            context.SetField("name", target.Name);
            context.Required();
            context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

            context.SetField("lastname", target.Lastname);
            context.Required();
            context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

            context.SetField("age", target.Age);
            context.Required(Errors.MUST_BE_NUMBER);
            context.MustBeGreaterThan(0, Errors.GREATER_ZERO);

            context.SetField("logins", target.Logins);
            context.Required();
            context.MustHaveCountBetween(1, 5, Errors.NUMBER_OF_LOGINS);
            this.loginvalidator.ValidateForMerge(target.Logins, context);

            context.Assert();
        }
    }
}