using System;

namespace Upida.Validation
{
    /// <summary>
    /// Defines base type validator members
    /// </summary>
    public interface IValidator
    {
        bool IsValid { get; }
        bool IsTargetValid { get; }
        bool IsFieldValid { get; }

        void SetTarget(Dtobase value);
        void SetIndex(int index);
        void SetField(string name, object value);

        void AddNested();
        void RemoveNested();

        void FailRoot(string msg);
        void Fail(string msg);
        void Fail(Failure failure);

        bool IsAssigned();
        bool IsNull();
        bool IsValidFormat();
        void MustHaveMinAssignedFieldsCount(int count, string msg);
        void MustHaveMaxAssignedFieldsCount(int count, string msg);
        void MustBeAssigned(string msg);
        void MustBeNotAssigned(string msg);
        void MustBeValidFormat(string msg);
        void MustBeNull(string msg);
        void MustBeNotNull(string msg);
        void MustEqualTo(object value, string msg);
        void MustNotEqualTo(object value, string msg);
        void MustEqualToOneOf(object[] values, string msg);
        void MustNotEqualToAnyOf(object[] values, string msg);
        void MustBeNotEmptyString(string msg);
        void MustHaveLengthBetween(int min, int max, string msg);
        void MustBeLessOrEqualTo(object m, string msg);
        void MustBeLessThan(object m, string msg);
        void MustBeGreaterOrEqualTo(object m, string msg);
        void MustBeGreaterThan(object m, String msg);
        void MustHaveCountBetween(int min, int max, string msg);
        void MustRegexpr(string expr, string msg);

        void Assert();
        IMath Math { get; }
    }
}