using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public static class ValidatorExtentions
    {
        /// <summary>
        /// Validates if field is assigned some value by parser and calls Stop on failure
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void MustBeAssigned<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (!validator.Target.IsFieldAssigned(validator.Name))
            {
                validator.Fail(msg);
                validator.Stop();
            }
        }

        /// <summary>
        /// Validates if field is clean - never assigned any value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void NotAssigned<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (validator.Target.IsFieldAssigned(validator.Name))
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field is correctly parsed and calls Stop on failure
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void ValidFormat<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (validator.Target.IsFieldWrong(validator.Name))
            {
                validator.Fail(msg);
                validator.Stop();
            }
        }

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void MustBeNull<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (null != validator.Value)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void MustEqualTo<T>(this ValidatorBase<T> validator, T value, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (!value.Equals(validator.Value))
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void NotEqualTo<T>(this ValidatorBase<T> validator, T value, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (value.Equals(validator.Value))
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void NotNull<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (null == validator.Value)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is not empty - (for string only)
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void NotEmpty<T>(this ValidatorBase<T> validator, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (string.Equals(string.Empty, validator.Value))
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates String length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void Length<T>(this ValidatorBase<T> validator, int min, int max, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            string val = (string)validator.Value;
            if (val.Length < min || val.Length > max)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void LessOrEqualTo<T>(this ValidatorBase<T> validator, object m, String msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (((IComparable)validator.Value).CompareTo(m) > 0)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void LessThan<T>(this ValidatorBase<T> validator, object m, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (((IComparable)validator.Value).CompareTo(m) >= 0)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void GreaterOrEqualTo<T>(this ValidatorBase<T> validator, object m, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (((IComparable)validator.Value).CompareTo(m) < 0)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void GreaterThan<T>(this ValidatorBase<T> validator, object m, String msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            if (((IComparable)validator.Value).CompareTo(m) <= 0)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates collection size is between min and max values inclusively (field must be a collection)
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void Size<T>(this ValidatorBase<T> validator, int min, int max, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            ICollection collection = (ICollection)validator.Value;
            if (collection.Count < min || collection.Count > max)
            {
                validator.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public static void Regexpr<T>(this ValidatorBase<T> validator, string expr, string msg)
            where T : Dtobase
        {
            if (validator.Stopped) return;

            Match match = Regex.Match((string)validator.Value, expr, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                validator.Fail(msg);
            }
        }
    }
}