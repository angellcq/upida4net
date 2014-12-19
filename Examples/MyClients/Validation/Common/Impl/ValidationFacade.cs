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
            IValidationContext context = this.ContextFactory.GetNew();
            context.SetTarget(target);
            this.ClientValidator.ValidateForSave(target, context);
            context.Assert();
        }

        public void AssertClientForUpdate(Client target)
        {
            IValidationContext context = this.ContextFactory.GetNew();
            context.SetTarget(target);
            this.ClientValidator.ValidateForUpdate(target, context);
            context.Assert();
        }

        public void AssertClientExists(Client item)
        {
            if (item == null)
            {
                IValidationContext context = this.ContextFactory.GetNew();
                context.Fail("Client does not exist");
                context.Assert();
            }
        }

        public void AssertMoreThanOneClient(long count)
        {
            if (count == 1)
            {
                IValidationContext context = this.ContextFactory.GetNew();
                context.Fail("Cannot delete the only client");
                context.Assert();
            }
        }
    }
}