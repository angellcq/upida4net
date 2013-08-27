using System;
using Upida;
using Upida.Validation;

namespace UpidaExample.Validation
{
    public static class MyExtendedValidatorExtentions
    {
        public static void Email<T>(this TypeValidatorBase<T> validator, string msg)
           where T : Dtobase
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            validator.Regexpr(expr, msg);
        }

        public static void Required<T>(this TypeValidatorBase<T> validator, string field, object value)
            where T : Dtobase
        {
            validator.Field(field, value);
            validator.MustBeAssigned(Errors.CANNOT_BE_EMPTY);
            validator.ValidFormat(Errors.WRONG_FORMAT);
            validator.NotNull(Errors.CANNOT_BE_EMPTY);
            validator.Stop();
        }

        public static void Missing<T>(this TypeValidatorBase<T> validator, string field, object value)
            where T : Dtobase
        {
            validator.Field(field, value);
            validator.NotAssigned(Errors.MUST_BE_EMPTY);
        }

        public static void ValidFormatOrNull<T>(this TypeValidatorBase<T> validator, string field, object value)
            where T : Dtobase
        {
            validator.Field(field, value);
            validator.ValidFormat(Errors.WRONG_FORMAT);
        }
    }
}