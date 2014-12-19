using System;
using Upida.Impl;

namespace MyClients.Validation.Common.Impl
{
    public class ValidationContextFactory : IValidationContextFactory
    {
        public IValidationContext GetNew()
        {
            return new ValidationContext(UpidaContext.Current);
        }
    }
}