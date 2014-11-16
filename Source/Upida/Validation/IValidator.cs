using System;

namespace Upida.Validation
{
    /// <summary>
    /// Defines base type validator members
    /// </summary>
    public interface IValidator
    {
        string FieldName { get; }

        string Path { get; }

        int? Index { get; }

        /// <summary>
        /// Returns list of failures for the target object
        /// </summary>
        /// <returns></returns>
        IFailureList Failures { get; }

        void SetParent(IValidator parent);

        /// <summary>
        /// Sets target validated object
        /// </summary>
        /// <param name="target">target validated object</param>
        /// <param name="parent">parent validator</param>
        void SetTarget(Dtobase target);

        void SetIndex(int index);

        /// <summary>
        /// Sets current validated field value and name.
        /// if value is not NULL, it automatically marks this field as assigned (even if it was not present in JSON)I
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <param name="value">field value</param>
        /// <returns></returns>
        void SetField(string name, object value);

        /// <summary>
        /// Sets current validated field name and value as NULL
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <returns></returns>
        void SetField(string name);

        bool IsAssigned();

        bool IsValidFormat();

        /// <summary>
        /// Sets severity of the next checking.
        /// Severity is reset to None after checking is done.
        /// </summary>
        /// <param name="severity"></param>
        void SetSeverity(Severity severity);

        /// <summary>
        /// Registers a failure against the current property.
        /// Current property is determined by the last call of the Field() method.
        /// </summary>
        /// <param name="msg">failure message</param>
        void FailRoot(string msg);

        /// <summary>
        /// Registers a failure against the current property.
        /// Current property is determined by the last call of the Field() method.
        /// </summary>
        /// <param name="msg">failure message</param>
        void Fail(string msg);

        /// <summary>
        /// Registers a failure against the current property's child property
        /// </summary>
        /// <param name="nestedField">child property</param>
        /// <param name="msg">failure message</param>
        void FailNested(string nestedField, string msg);

        /// <summary>
        /// Registers a failure against the current indexedt property's child property ( i.e. currentProperty[index].childProperty)
        /// </summary>
        /// <param name="index">index of the current property</param>
        /// <param name="nestedField">child property</param>
        /// <param name="msg">failure message</param>
        void FailNested(int index, string nestedField, string msg);

        /// <summary>
        /// Registers a failure (failure includes failure message and property path)
        /// </summary>
        /// <param name="failure">failure</param>
        void Fail(Failure failure);

        /// <summary>
        /// Validates if field is assigned some value by parser. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeAssigned(string msg);

        /// <summary>
        /// Validates if field is clean - never assigned any value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeNotAssigned(string msg);

        /// <summary>
        /// Validates if field is correctly parsed. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeValidFormat(string msg);

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeNull(string msg);

        /// <summary>
        /// Validates if field value is not null. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeNotNull(string msg);

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustEqualTo(object value, string msg);

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustNotEqualTo(object value, string msg);

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="values">destination values</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustEqualToOneOf(object[] values, string msg);

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="values">destination values</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustNotEqualToAnyOf(object[] values, string msg);

        /// <summary>
        /// Validates field value is not empty - (for string only)
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeNotEmptyString(string msg);

        /// <summary>
        /// Validates String length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustHaveLengthBetween(int min, int max, string msg);

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeLessOrEqualTo(object m, string msg);

        /// <summary>
        /// Validates field value is less than 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeLessThan(object m, string msg);

        /// <summary>
        /// Validates field value is greater than or equal to 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeGreaterOrEqualTo(object m, string msg);

        /// <summary>
        /// Validates field value is greater than 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustBeGreaterThan(object m, String msg);

        /// <summary>
        /// Validates collection size is between min and max values inclusively (field must implement ICollection)
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustHaveCountBetween(int min, int max, string msg);

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        void MustRegexpr(string expr, string msg);
    }
}