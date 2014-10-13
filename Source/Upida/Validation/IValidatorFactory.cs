using System;

namespace Upida.Validation
{
	public interface IValidatorFactory
	{
		/// <summary>
		/// Creates instance of the Type Validator
		/// </summary>
		/// <param name="typeValidatorType">type of the Type Validator</param>
		/// <returns>instance</returns>
		IValidatorBase GetInstance(Type typeValidatorType);
	}
}