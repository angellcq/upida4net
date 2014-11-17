using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation.Impl
{
    public class ClientValidator : IClientValidator
    {
        public IHandyValidatorFactory ValidatorFactory { get; set; }

        public void AssertValidForSave(Client target)
        {
            IHandyValidator context = this.ValidatorFactory.Get();
            context.SetTarget(target);

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

            if (context.IsFieldValid)
            {
                context.AddNested();
                int index = 0;
                foreach (Login login in target.Logins)
                {
                    context.SetIndex(index++);
                    context.SetTarget(login);
                    context.MissingField("id", login.Id);

                    context.SetField("name", target.Name);
                    context.Required();
                    context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                    context.SetField("password", login.Password);
                    context.Required();
                    context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                    context.SetField("enabled", login.Enabled);
                    context.Required(Errors.NOT_VALID_BOOL);

                    context.MissingField("client", login.Client);
                }
            }

            context.Assert();
        }

        public void AssertValidForUpdate(Client target)
        {
            IHandyValidator context = this.ValidatorFactory.Get();
            context.SetTarget(target);

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

            if (context.IsFieldValid)
            {
                context.AddNested();
                int index = 0;
                foreach (Login login in target.Logins)
                {
                    context.SetIndex(index++);
                    context.SetTarget(login);
                    context.SetField("id", login.Id);
                    context.RequiredIfAssigned();

                    context.SetField("name", login.Name);
                    context.Required();
                    context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                    context.SetField("password", login.Password);
                    context.Required();
                    context.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20);

                    context.SetField("enabled", login.Enabled);
                    context.Required(Errors.NOT_VALID_BOOL);

                    context.MissingField("client", login.Client);
                }
            }

            context.Assert();
        }
    }
}