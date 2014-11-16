using System;
using Upida.Validation;

namespace MyClients.Validation
{
    public interface IHandyValidator : IValidator
    {
        bool IsAssignedAndNotNull();

        void Required();

        void Required(string wrongFormatMessage);

        void RequiredIfAssigned();

        void MustBeEmail(string msg);

        void MissingField(string field, object value);
    }
}