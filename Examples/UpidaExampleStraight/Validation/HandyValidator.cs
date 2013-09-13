using System;
using Upida;
using Upida.Validation;

namespace UpidaExampleStraight.Validation
{
    public abstract class HandyValidator<T> : TypeConstraintValidatorBase<T>
        where T : Dtobase
    {
        public void Email(string msg)
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            this.Regexp(expr, msg);
        }

        public virtual void Required(string field, object value)
        {
            this.Field(field, value);
            this.MustBeAssigned(Errors.CANNOT_BE_EMPTY);
            this.ValidFormat(Errors.WRONG_FORMAT);
            this.NotNull(Errors.CANNOT_BE_EMPTY);
            this.Stop();
        }

        public virtual void Missing(string field, object value)
        {
            this.Field(field, value);
            this.NotAssigned(Errors.MUST_BE_EMPTY);
        }

        public virtual void ValidFormatOrNull(string field, object value)
        {
            this.Field(field, value);
            this.ValidFormat(Errors.WRONG_FORMAT);
        }
    }
}