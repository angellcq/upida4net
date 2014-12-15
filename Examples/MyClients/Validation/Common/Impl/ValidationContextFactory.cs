using System;

namespace MyClients.Validation.Common.Impl
{
    public class ValidationContextFactory : IValidationContextFactory
    {
        public IValidationContext Get()
        {
            return new ValidationContext();
        }
    }
}