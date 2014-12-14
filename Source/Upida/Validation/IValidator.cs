using System;

namespace Upida.Validation
{
    /// <summary>
    /// Defines base type validator members
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// False, if at least on failure is recorded, otherwise true
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// False, if at least on failure is recorded for the current target, otherwise true
        /// </summary>
        bool IsTargetValid { get; }

        /// <summary>
        /// False, if at least on failure is recorded for the current field, otherwise true
        /// </summary>
        bool IsFieldValid { get; }

        /// <summary>
        /// Sets current target object
        /// </summary>
        /// <param name="value">target object</param>
        void SetTarget(Dtobase value);

        /// <summary>
        /// Sets current index (used for indexed properties).
        /// For ex. current target is 'Children' and index is '7' and field 'Name' - failure would have path this path - 'children[7].name'.
        /// </summary>
        /// <param name="index">index</param>
        void SetIndex(int index);

        /// <summary>
        /// Sets current field (property) name
        /// </summary>
        /// <param name="name">field property name</param>
        /// <param name="value">field value</param>
        void SetField(string name, object value);

        /// <summary>
        /// Propogates current field to path.
        /// For ex. if current field is 'User', call AddNested(), then call SetField("Name", null) - the failure path would be - User.Name.
        /// Usually after AddNested() call, SetTarget() must be called as well.
        /// </summary>
        void AddNested();

        /// <summary>
        /// Goes onу node back in path
        /// </summary>
        void RemoveNested();

        /// <summary>
        /// Register failure without path
        /// </summary>
        /// <param name="msg">failure message</param>
        void FailRoot(string msg);

        /// <summary>
        /// Register failure using current path
        /// </summary>
        /// <param name="msg">failure message</param>
        void Fail(string msg);

        /// <summary>
        /// Register failure
        /// </summary>
        /// <param name="failure">failure object</param>
        void Fail(Failure failure);

        /// <summary>
        /// True if current field is assigned, otherwise false
        /// </summary>
        /// <returns>true is assigned</returns>
        bool IsAssigned();

        /// <summary>
        /// True is current field is null, otherwise false
        /// </summary>
        /// <returns>true is null</returns>
        bool IsNull();

        /// <summary>
        /// True if current field is correctly parsed, otherwise false
        /// </summary>
        /// <returns>true if valid</returns>
        bool IsValidFormat();

        /// <summary>
        /// Validates if number of assigned fields in the current target is greater or equal to min number
        /// </summary>
        /// <param name="count">min number of assigned fields</param>
        /// <param name="msg">failure message</param>
        void MustHaveMinAssignedFieldsCount(int count, string msg);

        /// <summary>
        /// Validates if number of assigned fields in the current target is less or equal to max number
        /// </summary>
        /// <param name="count">max number of assigned fields</param>
        /// <param name="msg">failure message</param>
        void MustHaveMaxAssignedFieldsCount(int count, string msg);

        /// <summary>
        /// Validates if current field is assigned (was present in incoming JSON)
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeAssigned(string msg);

        /// <summary>
        /// Validates if current field value is not assigned by JSON deserializer
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeNotAssigned(string msg);

        /// <summary>
        /// Validates if current field value is correctly parsed
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeValidFormat(string msg);

        /// <summary>
        /// Validates if current field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeNull(string msg);

        /// <summary>
        /// Validates if current field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeNotNull(string msg);

        /// <summary>
        /// Validates if current field value is equal to the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="msg">failure message</param>
        void MustEqualTo(object value, string msg);

        /// <summary>
        /// Validates if current field value is not equal to the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="msg">failure message</param>
        void MustNotEqualTo(object value, string msg);

        /// <summary>
        /// Validates if current field value is equal to one of the values
        /// </summary>
        /// <param name="values">the values</param>
        /// <param name="msg">failure message</param>
        void MustEqualToOneOf(object[] values, string msg);

        /// <summary>
        /// Validates if current field value is not equal to any of the values
        /// </summary>
        /// <param name="values">the values</param>
        /// <param name="msg">failure message</param>
        void MustNotEqualToAnyOf(object[] values, string msg);

        /// <summary>
        /// Validates if current field value is not string.Empty
        /// </summary>
        /// <param name="msg">failure message</param>
        void MustBeNotEmptyString(string msg);

        /// <summary>
        /// Validates if current field value length (string.Length) is between min and max
        /// </summary>
        /// <param name="min">min string.Length</param>
        /// <param name="max">max string.Length</param>
        /// <param name="msg">failure message</param>
        void MustHaveLengthBetween(int min, int max, string msg);

        /// <summary>
        /// Validates if current field value (IComparable) is less or equal to m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        void MustBeLessOrEqualTo(object m, string msg);

        /// <summary>
        /// Validates if current field value (IComparable) is less than m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        void MustBeLessThan(object m, string msg);

        /// <summary>
        /// Validates if current field value (IComparable) is greater or equal to m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        void MustBeGreaterOrEqualTo(object m, string msg);

        /// <summary>
        /// Validates if current field value (IComparable) is greater than m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        void MustBeGreaterThan(object m, String msg);

        /// <summary>
        /// Validates if current field value count (ICollection.Count) is between min and max count
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        void MustHaveCountBetween(int min, int max, string msg);

        /// <summary>
        /// Validates if current field value follows the regular expression
        /// </summary>
        /// <param name="expr">the regular expression</param>
        /// <param name="msg">failure message</param>
        void MustRegexpr(string expr, string msg);

        /// <summary>
        /// Throws ValidationException if at least one failure was registered
        /// </summary>
        void Assert();

        /// <summary>
        /// Gets Math helper object
        /// </summary>
        IMath Math { get; }
    }
}