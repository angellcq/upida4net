using System;
using MyClients.Domain;

namespace MyClients.Validation.Impl
{
    public class ValidationFacade : IValidationFacade
    {
        public IValidationHelperFactory HelperFactory { get; set; }
        public IClientValidator ClientValidator { get; set; }

        public void AssertClientForSave(Client target)
        {
            IHelper context = this.HelperFactory.Get();
            context.SetTarget(target);
            this.ClientValidator.ValidateForSave(target, context);
            context.Assert();
        }

        public void AssertClientForUpdate(Client target)
        {
            IHelper context = this.HelperFactory.Get();
            context.SetTarget(target);
            this.ClientValidator.ValidateForUpdate(target, context);
            context.Assert();
        }

        public void AssertClientExists(Client item)
        {
            IHelper context = this.HelperFactory.Get();
            if (item == null)
            {
                context.Fail("Client does not exist");
                context.Assert();
            }
        }

        public void AssertMoreThanOneClient(long count)
        {
            IHelper context = this.HelperFactory.Get();
            if (count == 1)
            {
                context.Fail("Cannot delete the only client");
                context.Assert();
            }
        }
    }
}