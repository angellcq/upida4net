using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents basic set of validation routines
    /// </summary>
    public class UpidaValidationContext : IUpidaValidationContext
    {
        private readonly IMath math;
        private readonly IPathHelper pathHelper;
        private readonly IChecker checker;
        private readonly LinkedList<PathNode> path;

        protected string fieldName;
        protected object fieldValue;
        protected IFailureList failures;

        private bool isValid;
        private bool isTargetValid;
        private bool isFieldValid;

        /// <summary>
        /// Initializes new instance of the ValidationContext class
        /// </summary>
        public UpidaValidationContext(IUpidaContext context)
        {
            this.math = context.Math;
            this.pathHelper = context.PathHelper;
            this.checker = context.Checker;
            this.path = this.pathHelper.CreateNew();
            this.isValid = true;
        }

        /// <summary>
        /// False, if at least on failure is recorded, otherwise true
        /// </summary>
        public bool IsValid
        {
            get { return this.isValid; }
        }

        /// <summary>
        /// False, if at least on failure is recorded for the current target, otherwise true
        /// </summary>
        public bool IsTargetValid
        {
            get { return this.isTargetValid; }
        }

        /// <summary>
        /// False, if at least on failure is recorded for the current field, otherwise true
        /// </summary>
        public bool IsFieldValid
        {
            get { return this.isFieldValid; }
        }

        /// <summary>
        /// Sets current target object
        /// </summary>
        /// <param name="value">target object</param>
        public void SetTarget(Dtobase value)
        {
            this.pathHelper.SetTopNodeTarget(this.path, value);
            this.isTargetValid = true;
        }

        /// <summary>
        /// Sets current index (used for indexed properties).
        /// For ex. current target is 'Children' and index is '7' and field 'Name' - failure would have this path - 'children[7].name'.
        /// </summary>
        /// <param name="index">index</param>
        public void SetIndex(int index)
        {
            this.path.Last.Value.Index = index;
        }

        /// <summary>
        /// Sets current field (property) name
        /// </summary>
        /// <param name="name">field property name</param>
        /// <param name="value">field value</param>
        public void SetField(string name, object value)
        {
            this.fieldValue = value;
            this.fieldName = name;
            this.isFieldValid = true;
        }

        /// <summary>
        /// Propagates current field to path.
        /// For ex. if current field is 'User', call AddNested(), then call SetField("Name", null) - the failure path would be - User.Name.
        /// Usually after AddNested() call, SetTarget() must be called as well.
        /// </summary>
        public void AddNested()
        {
            this.path.AddLast(new PathNode(this.fieldName));
        }

        /// <summary>
        /// Goes one node back in path.
        /// </summary>
        public void RemoveNested()
        {
            this.path.RemoveLast();
        }

        /// <summary>
        /// Registers failure without path
        /// </summary>
        /// <param name="msg">failure message</param>
        public void FailRoot(string msg)
        {
            this.Fail(new Failure(null, msg));
        }

        /// <summary>
        /// Registers failure using current path
        /// </summary>
        /// <param name="msg">failure message</param>
        public void Fail(string msg)
        {
            this.Fail(new Failure(this.pathHelper.BuildPath(this.path, this.fieldName), msg));
        }

        /// <summary>
        /// Registers failure
        /// </summary>
        /// <param name="failure">failure object</param>
        public void Fail(Failure failure)
        {
            this.isFieldValid = false;
            this.isTargetValid = false;
            this.isValid = false;
            if (null == this.failures)
            {
                this.failures = new FailureList();
            }

            this.failures.Fail(failure);
        }

        /// <summary>
        /// True if current field is assigned, otherwise false
        /// </summary>
        /// <returns>true is assigned</returns>
        public bool IsAssigned()
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            return this.checker.IsAssigned(this.fieldName, target);
        }

        /// <summary>
        /// True is current field is null, otherwise false
        /// </summary>
        /// <returns>true is null</returns>
        public bool IsNull()
        {
            return this.checker.IsNull(this.fieldValue);
        }

        /// <summary>
        /// True if current field is correctly parsed, otherwise false
        /// </summary>
        /// <returns>true if valid</returns>
        public bool IsValidFormat()
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            return this.checker.IsValidFormat(this.fieldName, target);
        }

        /// <summary>
        /// Validates if the number of assigned fields in the current target object is less or equal to max number
        /// </summary>
        /// <param name="count">max number of assigned fields</param>
        /// <param name="msg">failure message</param>
        public void MustHaveMaxAssignedFieldsCount(int count, string msg)
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            if (null != target.GetAssignedFields() &&
                count < target.GetAssignedFields().Count)
            {
                this.FailRoot(msg);
            }
        }

        /// <summary>
        /// Validates if the number of assigned fields in the current target object is greater or equal to min number
        /// </summary>
        /// <param name="count">min number of assigned fields</param>
        /// <param name="msg">failure message</param>
        public void MustHaveMinAssignedFieldsCount(int count, string msg)
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            if (null == target.GetAssignedFields() ||
                count > target.GetAssignedFields().Count)
            {
                this.FailRoot(msg);
            }
        }

        /// <summary>
        /// Validates if current field is assigned (was present in incoming JSON)
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeAssigned(string msg)
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            if (!this.checker.IsAssigned(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is not assigned by JSON deserializer
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeNotAssigned(string msg)
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            if (this.checker.IsAssigned(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is correctly parsed
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeValidFormat(string msg)
        {
            Dtobase target = this.pathHelper.GetTopNodeTarget(this.path);
            if (!this.checker.IsValidFormat(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeNull(string msg)
        {
            if (!this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeNotNull(string msg)
        {
            if (this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is equal to the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="msg">failure message</param>
        public void MustEqualTo(object value, string msg)
        {
            if (!this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is not equal to the value
        /// </summary>
        /// <param name="value">the value</param>
        /// <param name="msg">failure message</param>
        public void MustNotEqualTo(object value, string msg)
        {
            if (this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is equal to one of the values
        /// </summary>
        /// <param name="values">the values</param>
        /// <param name="msg">failure message</param>
        public void MustEqualToOneOf(object[] values, string msg)
        {
            if (!this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is not equal to any of the values
        /// </summary>
        /// <param name="values">the values</param>
        /// <param name="msg">failure message</param>
        public void MustNotEqualToAnyOf(object[] values, string msg)
        {
            if (this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value is not string.Empty
        /// </summary>
        /// <param name="msg">failure message</param>
        public void MustBeNotEmptyString(string msg)
        {
            if (null == this.fieldValue) return;
            if (this.checker.IsEmptyString((string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value length (string.Length) is between min and max
        /// </summary>
        /// <param name="min">min string.Length</param>
        /// <param name="max">max string.Length</param>
        /// <param name="msg">failure message</param>
        public void MustHaveLengthBetween(int min, int max, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsLengthBetween(min, max, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value (IComparable) is less or equal to m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        public void MustBeLessOrEqualTo(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsLessOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value (IComparable) is less than m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        public void MustBeLessThan(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (this.checker.IsLessThan(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value (IComparable) is greater or equal to m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        public void MustBeGreaterOrEqualTo(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsGreaterOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value (IComparable) is greater than m
        /// </summary>
        /// <param name="m">m</param>
        /// <param name="msg">failure message</param>
        public void MustBeGreaterThan(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsGreaterThan(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value count (ICollection.Count) is between min and max count
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        public void MustHaveCountBetween(int min, int max, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsCountBetween(min, max, (ICollection)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if current field value follows the regular expression
        /// </summary>
        /// <param name="expr">the regular expression</param>
        /// <param name="msg">failure message</param>
        public void MustRegexpr(string expr, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsRegexpr(expr, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Throws ValidationException if at least one failure was registered
        /// </summary>
        public void Assert()
        {
            if (null != this.failures)
            {
                throw new ValidationException(this.failures);
            }
        }

        /// <summary>
        /// Gets Math helper object
        /// </summary>
        public IMath Math
        {
            get { return this.math; }
        }
    }
}