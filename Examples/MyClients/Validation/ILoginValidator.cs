using MyClients.Domain;
using MyClients.Validation.Common;

namespace MyClients.Validation
{
    public interface ILoginValidator
    {
        void ValidateForSave(Login target, IValidationContext context);
        void ValidateForMerge(Login target, IValidationContext context);
    }
}