using System;
using Upida;

namespace MyClients.Validation
{
    public interface IHandyValidatorFactory
    {
        IHandyValidator Get();
    }
}