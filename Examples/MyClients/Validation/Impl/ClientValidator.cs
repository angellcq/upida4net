using System;
using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation.Impl
{
    public class ClientValidator : IClientValidator
    {
        public ILoginValidator LoginValidator { get; set; }

        public void ValidateForSave(Client target, IHelper context)
        {
            context.SetField("id", target.Id);
            context.Missing();

            context.SetField("name", target.Name);
            context.Required();
            context.Text();

            context.SetField("lastname", target.Lastname);
            context.Required();
            context.Text();

            context.SetField("age", target.Age);
            context.Required();
            context.Number();
            context.MustBeGreaterThan(0, "must be greater than zero");

            context.SetField("logins", target.Logins);
            context.Required();
            context.MustHaveCountBetween(1, 5, "must be at least one login");

            context.AddNested();
            int index = 0;
            foreach (Login login in target.Logins)
            {
                context.SetIndex(index++);
                context.SetTarget(login);
                this.LoginValidator.ValidateForSave(login, context);
            }

            context.RemoveNested();
        }

        public void ValidateForUpdate(Client target, IHelper context)
        {
            context.SetField("id", target.Id);
            context.Required();
            context.Number();

            context.SetField("name", target.Name);
            context.Required();
            context.Text();

            context.SetField("lastname", target.Lastname);
            context.Required();
            context.Text();

            context.SetField("age", target.Age);
            context.Required();
            context.Number();
            context.MustBeGreaterThan(0, "must be greater than zero");

            context.SetField("logins", target.Logins);
            context.Required();
            context.MustHaveCountBetween(1, 5, "must be at least one login");

            context.AddNested();
            int index = 0;
            foreach (Login login in target.Logins)
            {
                context.SetIndex(index++);
                context.SetTarget(login);
                this.LoginValidator.ValidateForMerge(login, context);
            }

            context.RemoveNested();
        }
    }
}