using System;
using System.Web.Mvc;

namespace Upida.Validation
{
	public class AspMvcTypeValidatorFactory : IValidatorFactory
	{
		public IValidatorBase GetInstance(Type typeValidatorType)
		{
			return (IValidatorBase)DependencyResolver.Current.GetService(typeValidatorType);
		}
	}
}