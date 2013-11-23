using System;
using System.Web.Mvc;
using Upida.Validation;

namespace Upida.Aspnetmvc
{
	public class AspMvcValidatorFactory : IValidatorFactory
	{
		public IValidatorBase GetInstance(Type typeValidatorType)
		{
			return (IValidatorBase)DependencyResolver.Current.GetService(typeValidatorType);
		}
	}
}