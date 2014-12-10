using System;

namespace MyClients.Validation.Impl
{
    public class ValidationHelperFactory : IValidationHelperFactory
    {
        public IHelper Get()
        {
            return new Helper();
        }
    }
}