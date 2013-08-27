using System;
using System.Web.Mvc;
using Upida.Validation;

namespace UpidaExample
{
    public class AutofacTypeValidatorFactory : ITypeValidatorFactory
    {
        public ITypeValidatorBase GetInstance(Type typeValidatorType)
        {
            return (ITypeValidatorBase)DependencyResolver.Current.GetService(typeValidatorType);
        }
    }
}