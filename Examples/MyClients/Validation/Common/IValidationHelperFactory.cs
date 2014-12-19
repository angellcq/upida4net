using System;
using Upida;

namespace MyClients.Validation.Common
{
    public interface IValidationContextFactory
    {
        IValidationContext GetNew();
    }
}