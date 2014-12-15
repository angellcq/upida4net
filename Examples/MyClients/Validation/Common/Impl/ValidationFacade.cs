using System;
using MyClients.Domain;

namespace MyClients.Validation.Common.Impl
{
    public class ValidationFacade : IValidationFacade
    {
        public IValidationContextFactory ContextFactory { get; set; }
        public IClientValidator ClientValidator { get; set; }

        public void AssertClientForSave(Client target)
        {
            IValidationContext context = this.ContextFactory.Get();
            context.SetTarget(target);
            this.ClientValidator.ValidateForSave(target, context);
            context.Assert();
        }

        public void AssertClientForUpdate(Client target)
        {
            IValidationContext context = this.ContextFactory.Get();
            context.SetTarget(target);
            this.ClientValidator.ValidateForUpdate(target, context);
            context.Assert();
        }

        public void AssertClientExists(Client item)
        {
            IValidationContext context = this.ContextFactory.Get();
            if (item == null)
            {
                context.Fail("Client does not exist");
                context.Assert();
            }
        }

        public void AssertMoreThanOneClient(long count)
        {
            IValidationContext context = this.ContextFactory.Get();
            if (count == 1)
            {
                context.Fail("Cannot delete the only client");
                context.Assert();
            }
        }
    }
}