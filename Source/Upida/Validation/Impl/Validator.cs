using System;
using System.Collections;
using System.Collections.Generic;
using Upida.Impl;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Represents basic set of type validator members
    /// </summary>
    public class Validator : IValidator
    {
        protected LinkedList<PathNode> path = new LinkedList<PathNode>();
        protected string fieldName;
        protected object fieldValue;
        protected Dtobase target;
        protected IFailureList failures;

        protected IMath math = UpidaContext.Current.Math;
        protected IPathHelper pathHelper = UpidaContext.Current.PathHelper;
        protected IChecker checker = UpidaContext.Current.Checker;

        private bool isValid;
        private bool isTargetValid;
        private bool isFieldValid;

        public Validator()
        {
            this.path.AddLast(new PathNode() { Name = string.Empty });
            this.isValid = true;
        }

        public bool IsValid
        {
            get { return this.isValid; }
        }

        public bool IsTargetValid
        {
            get { return this.isTargetValid; }
        }

        public bool IsFieldValid
        {
            get { return this.isFieldValid; }
        }


        public void SetTarget(Dtobase value)
        {
            this.target = value;
            this.isTargetValid = true;
        }

        public void SetIndex(int index)
        {
            this.path.Last.Value.Index = index;
        }

        public void SetField(string name, object value)
        {
            this.fieldValue = value;
            this.fieldName = name;
            this.isFieldValid = true;
        }

        public void AddNested()
        {
            this.path.AddLast(new PathNode() { Name = this.fieldName });
        }

        public void RemoveNested()
        {
            this.path.RemoveLast();
        }

        public void FailRoot(string msg)
        {
            this.Fail(new Failure(null, msg));
        }

        public void Fail(string msg)
        {
            this.Fail(new Failure(this.pathHelper.BuildPath(this.path, this.fieldName), msg));
        }

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


        public bool IsAssigned()
        {
            return this.checker.IsAssigned(this.fieldName, this.target);
        }

        public bool IsNull()
        {
            return this.checker.IsNull(this.fieldValue);
        }

        public bool IsValidFormat()
        {
            return this.checker.IsValidFormat(this.fieldName, this.target);
        }

        public void MustHaveMaxAssignedFieldsCount(int count, string msg)
        {
            if (null != this.target.GetAssignedFields() &&
                count < this.target.GetAssignedFields().Count)
            {
                this.FailRoot(msg);
            }
        }

        public void MustHaveMinAssignedFieldsCount(int count, string msg)
        {
            if (null == this.target.GetAssignedFields() ||
                count > this.target.GetAssignedFields().Count)
            {
                this.FailRoot(msg);
            }
        }

        public void MustBeAssigned(string msg)
        {
            if (!this.checker.IsAssigned(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        public void MustBeNotAssigned(string msg)
        {
            if (this.checker.IsAssigned(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        public void MustBeValidFormat(string msg)
        {
            if (!this.checker.IsValidFormat(this.fieldName, target))
            {
                this.Fail(msg);
            }
        }

        public void MustBeNull(string msg)
        {
            if (!this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeNotNull(string msg)
        {
            if (this.checker.IsNull(this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustEqualTo(object value, string msg)
        {
            if (!this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustNotEqualTo(object value, string msg)
        {
            if (this.checker.IsEqualTo(value, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustEqualToOneOf(object[] values, string msg)
        {
            if (!this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustNotEqualToAnyOf(object[] values, string msg)
        {
            if (this.checker.IsEqualToOneOf(values, this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeNotEmptyString(string msg)
        {
            if (null == this.fieldValue) return;
            if (this.checker.IsEmptyString((string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustHaveLengthBetween(int min, int max, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsLengthBetween(min, max, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeLessOrEqualTo(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsLessOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeLessThan(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (this.checker.IsLessOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeGreaterOrEqualTo(object m, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsGreaterOrEqualTo(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustBeGreaterThan(object m, String msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsGreaterThan(m, (IComparable)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustHaveCountBetween(int min, int max, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsCountBetween(min, max, (ICollection)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void MustRegexpr(string expr, string msg)
        {
            if (null == this.fieldValue) return;
            if (!this.checker.IsRegexpr(expr, (string)this.fieldValue))
            {
                this.Fail(msg);
            }
        }

        public void Assert()
        {
            if (null != this.failures)
            {
                throw new ValidationException(this.failures);
            }
        }

        public IMath Math
        {
            get { return this.math; }
        }
    }
}