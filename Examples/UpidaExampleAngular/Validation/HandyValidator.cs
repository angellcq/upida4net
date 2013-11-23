using Upida;
using Upida.Validation;

namespace UpidaExampleAngular.Validation
{
	public abstract class HandyValidator<T> : ConstraintValidator<T>
		where T : Dtobase
	{
		public void MustBeEmail(string msg)
		{
			const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
			this.MustRegexpr(expr, msg);
		}

		public virtual void Required()
		{
			this.MustBeAssigned(Errors.REQUIRED);
			this.MustBeNotNull(Errors.REQUIRED);
		}

		public virtual void Required(string wrongFormatMessage)
		{
			this.MustBeAssigned(Errors.REQUIRED);
			this.MustBeValidFormat(wrongFormatMessage);
			this.MustBeNotNull(Errors.REQUIRED);
		}

		public virtual void MissingField(string field, object value)
		{
			this.Field(field, value);
			this.MustBeNotAssigned(Errors.MUST_BE_EMPTY);
		}
	}
}