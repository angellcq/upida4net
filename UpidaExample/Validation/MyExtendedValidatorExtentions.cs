using System;
using Upida;
using Upida.Validation;

namespace UpidaExample.Validation
{
    public static class MyExtendedValidatorExtentions
    {
        public static void CreditCard<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            const string expr = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";
            validator.Regexpr(expr, msg);
        }

        public static void Email<T>(this ValidatorBase<T> validator, string msg)
           where T : Dtobase
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            validator.Regexpr(expr, msg);
        }

        public static void Required<T>(this ValidatorBase<T> validator)
            where T : Dtobase
        {
            validator.MustBeAssigned(Errors.CANNOT_BE_EMPTY);
            validator.Stop();
            validator.NotNull(Errors.CANNOT_BE_EMPTY);
            validator.Stop();
        }

        public static void RequiredNumber<T>(this ValidatorBase<T> validator)
            where T : Dtobase
        {
            validator.MustBeAssigned(Errors.CANNOT_BE_EMPTY);
            validator.Stop();
            validator.ValidFormat(Errors.INVALID_NUMBER);
            validator.Stop();
            validator.NotNull(Errors.CANNOT_BE_EMPTY);
            validator.Stop();
        }

        public static void MissingField<T>(this ValidatorBase<T> validator, string field)
            where T : Dtobase
        {
            validator.Field(field);
            validator.MustBeUnassigned(Errors.MUST_BE_EMPTY);
        }

        public static void ValidNumberOrNull<T>(this ValidatorBase<T> validator)
            where T : Dtobase
        {
            if (validator.Target.IsFieldAssigned(validator.Name))
            {
                validator.ValidFormat(Errors.INVALID_NUMBER);
                validator.Stop();
            }
        }
    }
}