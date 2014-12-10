using MyClients.Domain;
using Upida.Validation;

namespace MyClients.Validation
{
    public interface IClientValidator
    {
        void ValidateForSave(Client target, IHelper context);
        void ValidateForUpdate(Client target, IHelper context);
    }
}