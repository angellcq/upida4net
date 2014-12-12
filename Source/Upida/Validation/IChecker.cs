using System;
using System.Collections;

namespace Upida.Validation
{
    public interface IChecker
    {
        /// <summary>
        /// Checks if field value is assigned
        /// </summary>
        /// <returns></returns>
        bool IsAssigned(string fieldName, Dtobase target);

        /// <summary>
        /// Checks if field value was correctly parsed
        /// </summary>
        /// <returns></returns>
        bool IsValidFormat(string fieldName, Dtobase target);

        /// <summary>
        /// Checks if field value is NULL
        /// </summary>
        /// <returns></returns>
        bool IsNull(object value);

        /// <summary>
        /// Checks if field value equals to m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="value">field value</param>
        /// <returns></returns>
        bool IsEqualTo(object m, object value);

        /// <summary>
        /// Checks if field value is equal to one of values
        /// </summary>
        /// <returns></returns>
        bool IsEqualToOneOf(object[] values, object value);

        /// <summary>
        /// Checks if field value is empty string
        /// </summary>
        /// <returns></returns>
        bool IsEmptyString(string value);

        /// <summary>
        /// Checks if field value length is between min and max values
        /// </summary>
        /// <returns></returns>
        bool IsLengthBetween(int min, int max, string value);

        /// <summary>
        /// Checks if field value is less than or equal to 'm'
        /// </summary>
        /// <returns></returns>
        bool IsLessOrEqualTo(object m, IComparable value);

        /// <summary>
        /// Checks if field value is less than 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        bool IsLessThan(object m, IComparable value);

        /// <summary>
        /// Checks if field value is greater than or equal to 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        bool IsGreaterOrEqualTo(object m, IComparable value);

        /// <summary>
        /// Checks if field value is greater than 'm'. IComparable.
        /// </summary>
        /// <returns></returns>
        bool IsGreaterThan(object m, IComparable value);

        /// <summary>
        /// Checks if field value collection size is between min and max values inclusively (field must implement ICollection)
        /// </summary>
        /// <returns></returns>
        bool IsCountBetween(int min, int max, ICollection value);

        /// <summary>
        /// Checks if field value qualifies to regular expression
        /// </summary>
        /// <returns></returns>
        bool IsRegexpr(string expr, string value);
    }
}