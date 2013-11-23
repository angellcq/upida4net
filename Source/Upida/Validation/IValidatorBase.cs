using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	public interface IValidatorBase
	{
		void Fail(string msg);
		void Fail(Failure failure);
		void Validate(object state);
	}
}