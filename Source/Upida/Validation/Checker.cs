using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public class Checker
    {
        /// <summary>
        /// Checks if field value is assigned
        /// </summary>
        /// <returns></returns>
        public bool IsAssigned(string fieldName, Dtobase target)
        {
            return target.IsFieldAssigned(fieldName);
        }

        /// <summary>
        /// Checks if field value was correctly parsed
        /// </summary>
        /// <returns></returns>
        public bool IsValidFormat(string fieldName, Dtobase target)
        {
            return !target.IsFieldWrong(fieldName);
        }

        /// <summary>
        /// Checks if field value is NULL
        /// </summary>
        /// <returns></returns>
        public bool IsNull(object value)
        {
            return null == value;
        }

        public bool IsEqualTo(object m, object value)
        {
            return object.Equals(m, value);
        }

        /// <summary>
        /// Checks if field value is equal to one of values
        /// </summary>
        /// <returns></returns>
        public bool IsEqualToOneOf(object[] values, object value)
        {
            bool success = false;
            for (int i = 0; i < values.Length; i++)
            {
                if (object.Equals(values[i], value))
                {
                    success = true;
                    break;
                }
            }

            return success;
        }

        /// <summary>
        /// Checks if field value is empty string
        /// </summary>
        /// <returns></returns>
        public virtual bool IsEmptyString(string value)
        {
            return string.Equals(string.Empty, value);
        }

        /// <summary>
        /// Checks if field value length is between min and max values
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLengthBetween(int min, int max, string value)
        {
            return value.Length >= min && value.Length <= max;
        }

        /// <summary>
        /// Checks if field value is less than or equal to 'm'
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLessOrEqualTo(object m, IComparable value)
        {
            return value.CompareTo(m) <= 0;
        }

        /// <summary>
        /// Checks if field value is less than 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsLessThan(object m, IComparable value)
        {
            return value.CompareTo(m) < 0;
        }

        /// <summary>
        /// Checks if field value is greater than or equal to 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsGreaterOrEqualTo(object m, IComparable value)
        {
            return value.CompareTo(m) >= 0;
        }

        /// <summary>
        /// Checks if field value is greater than 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsGreaterThan(object m, IComparable value)
        {
            return value.CompareTo(m) > 0;
        }

        /// <summary>
        /// Checks if field value collection size is between min and max values inclusively (field must implement ICollection)
        /// </summary>
        /// <returns></returns>
        public bool IsCountBetween(int min, int max, ICollection value)
        {
            return value.Count >= min && value.Count <= max;
        }

        /// <summary>
        /// Checks if field value qualifies to regular expression
        /// </summary>
        /// <returns></returns>
        public bool IsRegexpr(string expr, string value)
        {
            Match match = Regex.Match(value, expr, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}