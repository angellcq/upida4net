using System;
using MyClients.Domain;

namespace MyClients.Validation
{
    public interface IValidationFacade
    {
        void AssertClientForSave(Client target);
        void AssertClientForUpdate(Client target);
        void AssertClientExists(Client item);
        void AssertMoreThanOneClient(long count);
    }
}