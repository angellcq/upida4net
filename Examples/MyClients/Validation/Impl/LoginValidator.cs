using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyClients.Domain;

namespace MyClients.Validation.Impl
{
    public class LoginValidator : ILoginValidator
    {
        public void ValidateForSave(Login target, IHelper context)
        {
            context.SetField("id", target.Id);
            context.Missing();

            context.SetField("name", target.Name);
            context.Required();
            context.Text();

            context.SetField("password", target.Password);
            context.Required();
            context.Text();

            context.SetField("enabled", target.Enabled);
            context.TrueFalse();

            context.SetField("client", target.Client);
            context.Missing();
        }

        public void ValidateForMerge(Login target, IHelper context)
        {
            context.SetField("id", target.Id);
            if (context.IsAssigned())
            {
                context.Number();
            }

            context.SetField("name", target.Name);
            context.Required();
            context.Text();

            context.SetField("password", target.Password);
            context.Required();
            context.Text();

            context.SetField("enabled", target.Enabled);
            context.TrueFalse();

            context.SetField("client", target.Client);
            context.Missing();
        }
    }
}