using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public static class ValidatorExtentions
    {
        /// <summary>
        /// Validates if field is assigned some value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> MustBeAssigned<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (!validator.Target.isFieldAssigned(validator.Name))
            {
                validator.Fail(msg);
            }

            return validator;
        }

        /// <summary>
        /// Validates if field is clean - never assigned any value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> MustBeUnassigned<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (validator.Target.isFieldAssigned(validator.Name))
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates if field is correctly parsed
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> ValidFormat<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (validator.Target.isFieldWrong(validator.Name))
            {
                validator.Fail(msg);
            }

            return validator;
        }

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> MustBeNull<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (null != validator.Value)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> MustEqualTo<T>(this ValidatorBase<T> validator, T value, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (!value.Equals(validator.Value))
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> NotEqualTo<T>(this ValidatorBase<T> validator, T value, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (value.Equals(validator.Value))
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> NotNull<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            if (null == validator.Value)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates String length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> Length<T>(this ValidatorBase<T> validator, int min, int max, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            string val = (string)validator.Value;
            if (val.Length < min || val.Length > max)
            {
                validator.Fail(msg);
            }

            return validator;
        }

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> LessOrEqualTo<T>(this ValidatorBase<T> validator, Object m, String msg)
            where T : Dtobase
        {
            if (validator.Stopped || null == m) return validator;

            if (((IComparable)validator.Value).CompareTo(m) > 0)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates field value is less than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> LessThan<T>(this ValidatorBase<T> validator, object m, string msg)
            where T : Dtobase
        {
            if (validator.Stopped || null == m) return validator;

            if (((IComparable)validator.Value).CompareTo(m) >= 0)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates field value is greater than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> GreaterOrEqualTo<T>(this ValidatorBase<T> validator, object m, string msg)
            where T : Dtobase
        {
            if (validator.Stopped || null == m) return validator;

            if (((IComparable)validator.Value).CompareTo(m) < 0)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates field value is greater than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> GreaterThan<T>(this ValidatorBase<T> validator, object m, String msg)
            where T : Dtobase
        {
            if (validator.Stopped || null == m) return validator;

            if (((IComparable)validator.Value).CompareTo(m) <= 0)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates collection size is between min and max values inclusively (field must be a collection)
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> Size<T>(this ValidatorBase<T> validator, int min, int max, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return validator;

            ICollection collection = (ICollection)validator.Value;
            if (collection.Count < min || collection.Count > max)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static ValidatorBase<T> Regexpr<T>(this ValidatorBase<T> validator, string expr, string msg)
            where T : Dtobase
        {
            Match match = Regex.Match((string)validator.Value, expr, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                validator.Fail(msg);
            }
            return validator;
        }

        public static ValidatorBase<T> CreditCard<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            const string expr = @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$";
            return validator.Regexpr(expr, msg);
        }

        public static ValidatorBase<T> Email<T>(this ValidatorBase<T> validator, string msg)
           where T : Dtobase
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            return validator.Regexpr(expr, msg);
        }
    }
}