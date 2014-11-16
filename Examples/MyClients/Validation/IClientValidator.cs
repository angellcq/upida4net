using MyClients.Domain;

namespace MyClients.Validation
{
    public interface IClientValidator
    {
        void ValidateForSave(Client target);

        void ValidateForUpdate(Client target);
    }
}