using System;
using Upida;

namespace MyClients.Validation
{
    public interface IValidationHelperFactory
    {
        IHelper Get();
    }
}