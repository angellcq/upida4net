using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation
{
    public interface IClientValidator
    {
        void AssertValidForSave(Client target, IValidator context);

        void AssertValidForUpdate(Client target, IValidator context);
    }
}