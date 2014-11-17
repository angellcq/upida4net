using MyClients.Domain;

namespace MyClients.Validation
{
    public interface IClientValidator
    {
        void AssertValidForSave(Client target);

        void AssertValidForUpdate(Client target);
    }
}