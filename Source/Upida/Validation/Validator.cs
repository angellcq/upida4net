using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation
{
    /// <summary>
    /// Represents basic set of type validator members
    /// </summary>
    public abstract class Validator : IValidator
    {
        protected LinkedList<PathNode> path = new LinkedList<PathNode>();
        protected string fieldName;
        protected object fieldValue;
        protected Dtobase target;
        protected Severity severity;
        protected IValidator parent;
        protected IFailureList failures;

        protected PathHelper pathHelper = UpidaContext.Current.PathHelper;
        protected Checker checker = UpidaContext.Current.Checker;

        /// <summary>
        /// Represents name of the current field
        /// </summary>
        public string FieldName
        {
            get { return this.fieldName; }
        }

        /// <summary>
        /// Returns list of failures for the target object
        /// </summary>
        /// <returns></returns>
        public IFailureList Failures
        {
            get { return this.failures ?? new FailureList(); }
        }

        public void SetIndex(int index)
        {
            this.path.Last.Value.Index = index;
        }

        /// <summary>
        /// Sets current validated field value and name.
        /// if value is not NULL, it automatically marks this field as assigned (even if it was not present in JSON)I
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <param name="value">field value</param>
        /// <returns></returns>
        public void SetField(string name, object value)
        {
            this.fieldValue = value;
            this.fieldName = name;
            this.severity = Severity.None;
        }

        /// <summary>
        /// Sets current validated field name and value as NULL
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <returns></returns>
        public void SetField(string name)
        {
            this.SetField(name, null);
        }

        public bool IsAssigned()
        {
            return this.checker.IsAssigned(this.fieldName, this.target);
        }

        public bool IsValidFormat()
        {
            return this.checker.IsValidFormat(this.fieldName, this.target);
        }

        public void AddNested()
        {
            this.path.AddLast(new PathNode() { Name = this.fieldName });
        }

        public void RemoveNested()
        {
            this.path.RemoveLast();
        }

        /// <summary>
        /// Sets severity of the next checking.
        /// Severity is reset to None after checking is done.
        /// </summary>
        /// <param name="severity"></param>
        public void SetSeverity(Severity severity)
        {
            this.severity = severity;
        }

        /// <summary>
        /// Registers a failure against the current property.
        /// Current property is determined by the last call of the Field() method.
        /// </summary>
        /// <param name="msg">failure message</param>
        public void FailRoot(string msg)
        {
            this.Fail(new Failure(null, msg, this.severity));
        }

        /// <summary>
        /// Registers a failure against the current property.
        /// Current property is determined by the last call of the Field() method.
        /// </summary>
        /// <param name="msg">failure message</param>
        public void Fail(string msg)
        {
            this.Fail(new Failure(this.pathHelper.BuildPath(this.path, this.fieldName), msg, this.severity));
        }

        /// <summary>
        /// Registers a failure (failure includes failure message and property path)
        /// </summary>
        /// <param name="failure">failure</param>
        public void Fail(Failure failure)
        {
            if (null == this.parent)
            {
                if (null == this.failures)
                {
                    this.failures = new FailureList();
                }

                this.failures.Fail(failure);
            }
            else
            {
                this.parent.Fail(failure);
            }
        }

        /// <summary>
        /// Validates if field is assigned some value by parser. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeAssigned(string msg)
        {
            if (!this.checker.IsAssigned(this.fieldName, this.target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field is clean - never assigned any value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeNotAssigned(string msg)
        {
            if (this.checker.IsAssigned(this.fieldName, this.target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field is correctly parsed. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeValidFormat(string msg)
        {
            if (!this.checker.IsValidFormat(this.fieldName, this.target))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeNull(string msg)
        {
            if (!this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is not null. Stops if failed.
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeNotNull(string msg)
        {
            if (this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustEqualTo(object value, string msg)
        {
            if (!this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustNotEqualTo(object value, string msg)
        {
            if (this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="values">destination values</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustEqualToOneOf(object[] values, string msg)
        {
            if (!this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="values">destination values</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustNotEqualToAnyOf(object[] values, string msg)
        {
            if (this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is not empty - (for string only)
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeNotEmptyString(string msg)
        {
            if (this.checker.IsEmptyString((string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates String length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustHaveLengthBetween(int min, int max, string msg)
        {
            if (this.checker.IsLengthBetween(min, max, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeLessOrEqualTo(object m, string msg)
        {
            if (!this.checker.IsLessOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeLessThan(object m, string msg)
        {
            if (this.checker.IsLessOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than or equal to 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeGreaterOrEqualTo(object m, string msg)
        {
            if (!this.checker.IsGreaterOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than 'm'. IComparable.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustBeGreaterThan(object m, String msg)
        {
            if (!this.checker.IsGreaterThan(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates collection size is between min and max values inclusively (field must implement ICollection)
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustHaveCountBetween(int min, int max, string msg)
        {
            if (!this.checker.IsCountBetween(min, max, (ICollection)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public void MustRegexpr(string expr, string msg)
        {
            if (!this.checker.IsRegexpr(expr, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }
    }
}