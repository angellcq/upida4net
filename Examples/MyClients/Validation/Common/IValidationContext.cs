using System;
using Upida.Validation;

namespace MyClients.Validation.Common
{
    public interface IValidationContext : IUpidaValidationContext
    {
        void Required();
        void Missing();
        void Number();
        void Date();
        void Float();
        void Email();
        void Text();
        void TrueFalse();
    }
}