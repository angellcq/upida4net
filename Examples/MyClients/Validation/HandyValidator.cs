﻿using Upida;
using Upida.Validation;

namespace MyClients.Validation
{
	public class HandyValidator<T> : ConstraintValidator<T>
			where T : Dtobase
	{
		protected HandyValidator<T> self;

		public HandyValidator()
		{
			this.self = this;
		}

		public void SetSelf(HandyValidator<T> self)
		{
			this.self = self;
		}

		public virtual void Required()
		{
			self.MustBeAssigned(Errors.REQUIRED);
			self.MustBeNotNull(Errors.REQUIRED);
		}

		public virtual void Required(string wrongFormatMessage)
		{
			self.MustBeAssigned(Errors.REQUIRED);
			self.MustBeValidFormat(wrongFormatMessage);
			self.MustBeNotNull(Errors.REQUIRED);
		}

		public virtual void RequiredIfAssigned()
		{
			if (self.IsAssigned())
			{
				self.Required(Errors.MUST_BE_NUMBER);
			}
		}

		public void MustBeEmail(string msg)
		{
			const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
			self.MustRegexpr(expr, msg);
		}

		public virtual void MissingField(string field, object value)
		{
			self.Field(field, value);
			self.MustBeNotAssigned(Errors.MUST_BE_EMPTY);
		}

		public override void Validate(object state)
		{
		}
	}
}