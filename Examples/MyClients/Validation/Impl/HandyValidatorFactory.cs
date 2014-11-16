using System;
using System.Web.Http;
using Upida;

namespace MyClients.Validation.Impl
{
    public class HandyValidatorFactory : IHandyValidatorFactory
    {
        public IHandyValidator Get(IHandyValidator parent)
        {
            HandyValidator item = new HandyValidator();
            item.SetParent(parent);
            return item;
        }
    }
}