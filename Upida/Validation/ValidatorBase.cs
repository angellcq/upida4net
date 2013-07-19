using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public abstract class ValidatorBase<T> : IValidatorBase
        where T : Dtobase
    {
        private string path;
        private string name;
        private object value;
        private IValidatorBase parent;
        protected T target;

        private bool validTarget;
        private bool validField;
        private bool stopped;
        private IList<Failure> failures;

        public IList<Failure> GetFailures()
        {
            return this.failures;
        }

        public void SetTarget(T target, string path, IValidatorBase parent)
        {
            this.target = target;
            this.path = path;
            this.parent = parent;
            this.validTarget = true;
            this.failures = null;
        }

        /// <summary>
        /// Sets current validated field value and name
        /// </summary>
        /// <param name="value">field value</param>
        /// <param name="name">name of the field</param>
        /// <returns></returns>
        protected ValidatorBase<T> Field(object value, string name)
        {
            this.value = value;
            this.name = name;
            this.stopped = false;
            this.validField = true;
            return this;
        }

        /// <summary>
        /// Disables validation for the current field if it is allready failed
        /// </summary>
        /// <returns></returns>
        public ValidatorBase<T> Stop()
        {
            if(!this.validField)
            {
                this.stopped = true;
            }
            return this;
        }

        public ValidatorBase<T> MustBeAssigned(string msg)
        {
            if(this.stopped) return this;

            if(!this.target.isFieldAssigned(this.name))
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field is assigned some value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> MustBeUnassigned(string msg)
        {
            if(this.stopped) return this;

            if(this.target.isFieldAssigned(this.name))
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field is correctly parsed
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> ValidFormat(string msg)
        {
            if(this.stopped) return this;

            if(this.target.isFieldWrong(this.name))
            {
                this.Fail(msg);
            }

            return this;
        }

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> MustBeNull(string msg)
        {
            if(this.stopped) return this;

            if(null != this.value)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> MustEqualTo(T value, string msg)
        {
            if(this.stopped) return this;

            if(!value.Equals(this.value))
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> NotEqualTo(T value, string msg)
        {
            if(this.stopped) return this;

            if(value.Equals(this.value))
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> NotNull(string msg)
        {
            if(this.stopped) return this;

            if(null == this.value)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates String's length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> Length(int min, int max, string msg)
        {
            if(this.stopped) return this;

            string val = (string)this.value;
            if(val.Length < min || val.Length > max)
            {
                this.Fail(msg);
            }

            return this;
        }

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> LessOrEqualTo(Object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if(((IComparable)this.value).CompareTo(m) > 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field value is less than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> LessThan(object m, string msg)
        {
            if(this.stopped || null == m) return this;

            if (((IComparable)this.value).CompareTo(m) >= 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field value is greater than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> GreaterOrEqualTo(object m, string msg)
        {
            if(this.stopped || null == m) return this;

            if (((IComparable)this.value).CompareTo(m) < 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates field value is greater than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> GreaterThan(object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if(((IComparable)this.value).CompareTo(m) <= 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates collection size is between min and max values inclusively
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> Size(int min, int max, string msg)
        {
            if(this.stopped) return this;

            ICollection collection = (ICollection)this.value;
            if (collection.Count < min || collection.Count > max)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public ValidatorBase<T> Regexpr(string expr, string msg)
        {
            Match match = Regex.Match((string)this.value, expr, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                this.Fail(msg);
            }
            return this;
        }

        /// <summary>
        /// Triggers validation on nested object against specific group
        /// </summary>
        /// <typeparam name="R">Type of the validated object</typeparam>
        /// <param name="group">validation group</param>
        /// <returns></returns>
        public ValidatorBase<T> Nested<R>(int group)
            where R : Dtobase
        {
            if (this.stopped) return this;

            ValidatorBase<R> nestedValidator = FluentAttribute.BuildValidator<R>(group);
            if (null != nestedValidator)
            {
                string fullPath = string.Concat(this.path, this.name, ".");
                nestedValidator.SetTarget((this.value as R), fullPath, this);
                nestedValidator.Validate();
            }

            return this;
        }

        /// <summary>
        /// Triggers validation on each object from the nested colection of objects against specific group
        /// </summary>
        /// <typeparam name="R">Type of the validated object</typeparam>
        /// <param name="group">validation group</param>
        /// <returns></returns>
        public ValidatorBase<T> NestedList<R>(int group)
            where R : Dtobase
        {
            if (this.stopped) return this;

            ValidatorBase<R> nestedValidator = FluentAttribute.BuildValidator<R>(group);
            if (null != nestedValidator)
            {
                int index = 0;
                foreach (R nestedTarget in (this.value as IEnumerable))
                {
                    string fullPath = string.Concat(this.path, this.name, "[", index, "].");
                    nestedValidator.SetTarget(nestedTarget, fullPath, this);
                    nestedValidator.Validate();
                    index++;
                }
            }

            return this;
        }

        public bool IsValidField
        {
            get { return this.validField; }
        }

        public bool IsValid
        {
            get { return this.validTarget; }
        }

        /// <summary>
        /// Refisters a failure against the current property.
        /// Current property is determined by the last call of the Field() method.
        /// </summary>
        /// <param name="msg">failure message</param>
        public void Fail(string msg)
        {
            this.Fail(new Failure(string.Concat(this.path, this.name), msg));
            this.validField = false;
        }

        /// <summary>
        /// Registers a failure (failure includes failure message and property path)
        /// </summary>
        /// <param name="failure">failure</param>
        public void Fail(Failure failure)
        {
            if (this.stopped) return;

            this.validTarget = false;
            if (null == this.parent)
            {
                if (null == this.failures)
                {
                    this.failures = new List<Failure>();
                }

                this.failures.Add(failure);
            }
            else
            {
                this.parent.Fail(failure);
            }
        }

        public abstract void Validate();
    }
}