using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	/// <summary>
	/// Defines base type validator members
	/// </summary>
	public interface IValidatorBase
	{
		/// <summary>
		/// Registers a failure
		/// </summary>
		/// <param name="msg">failure message</param>
		void Fail(string msg);

		/// <summary>
		/// Registers a failure
		/// </summary>
		/// <param name="failure">failure object</param>
		void Fail(Failure failure);

		/// <summary>
		/// Validates current object and passes state information
		/// </summary>
		/// <param name="state">state information</param>
		void Validate(object state);
	}
}