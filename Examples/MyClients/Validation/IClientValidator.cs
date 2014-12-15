using MyClients.Domain;
using MyClients.Validation.Common;
using Upida.Validation;

namespace MyClients.Validation
{
    public interface IClientValidator
    {
        void ValidateForSave(Client target, IValidationContext context);
        void ValidateForUpdate(Client target, IValidationContext context);
    }
}