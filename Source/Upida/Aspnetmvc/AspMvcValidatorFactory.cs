using System;
using System.Web.Mvc;
using Upida.Validation;

namespace Upida.Aspnetmvc
{
	/// <summary>
	/// Validator Factory used in ASP.Net Mvc project
	/// </summary>
	public class AspMvcValidatorFactory : IValidatorFactory
	{
		/// <summary>
		/// Creates instance of the Type Validator
		/// </summary>
		/// <param name="typeValidatorType">Type Validator class</param>
		/// <returns></returns>
		public IValidatorBase GetInstance(Type typeValidatorType)
		{
			return (IValidatorBase)DependencyResolver.Current.GetService(typeValidatorType);
		}
	}
}