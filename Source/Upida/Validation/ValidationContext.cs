using System;
using System.Collections.Generic;

namespace Upida.Validation
{
	public class ValidationContext : IValidationContext
	{
		public IFailureList Validate<T>(T target, object group) where T : Dtobase
		{
			return this.Validate(target, group, null);
		}

		public IFailureList Validate<T>(T target, object group, object state) where T : Dtobase
		{
			ValidatorBase<T> validator = UpidaContext.Current().BuildValidator<T>(group);
			if (null != validator)
			{
				validator.SetTarget(target, null, null);
				validator.Validate(state);
				return validator.GetFailures();
			}
			else
			{
				throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
			}
		}

		public void AssertValid<T>(T target, object group) where T : Dtobase
		{
			this.AssertValid(target, group, null);
		}

		public void AssertValid<T>(T target, object group, object state) where T : Dtobase
		{
			ValidatorBase<T> validator = UpidaContext.Current().BuildValidator<T>(group);
			if (null != validator)
			{
				validator.SetTarget(target, null, null);
				validator.Validate(null);

				if (!validator.IsValid)
				{
					throw new ValidationException(validator.GetFailures(), typeof(T), group);
				}
			}
			else
			{
				throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
			}
		}

		public IFailureList CreateFailureList()
		{
			return new FailureList();
		}

		public void Assert(IFailureList errors)
		{
			if (null != errors && !errors.IsEmpty)
			{
				throw new ValidationException(errors);
			}
		}
	}
}