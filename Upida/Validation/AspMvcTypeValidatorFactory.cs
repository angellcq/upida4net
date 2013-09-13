using System;
using System.Web.Mvc;

namespace Upida.Validation
{
    public class AspMvcTypeValidatorFactory : ITypeValidatorFactory
    {
        public ITypeValidatorBase GetInstance(Type typeValidatorType)
        {
            return (ITypeValidatorBase)DependencyResolver.Current.GetService(typeValidatorType);
        }
    }
}