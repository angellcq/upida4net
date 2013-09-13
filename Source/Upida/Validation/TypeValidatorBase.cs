using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public abstract class TypeValidatorBase<T> : ITypeValidatorBase
        where T : Dtobase
    {
        private string path;
        private string name;
        private object value;
        private ITypeValidatorBase parent;
        private T target;

        private bool validTarget;
        private bool validField;
        private bool stopped;
        private IList<Failure> failures;

        /// <summary>
        /// True if current field is valid so far
        /// </summary>
        public bool IsValidField
        {
            get { return this.validField; }
        }

        /// <summary>
        /// True is the target object is valid so far
        /// </summary>
        public bool IsValid
        {
            get { return this.validTarget; }
        }

        /// <summary>
        /// True if validation is stopped for the current field. You can stop validation for current field only if Stop() method is called and the field is invalid
        /// </summary>
        public bool Stopped
        {
            get { return this.stopped; }
        }

        /// <summary>
        /// Represents target object
        /// </summary>
        public T Target
        {
            get { return this.target; }
        }

        /// <summary>
        /// Represents value of the current field
        /// </summary>
        public object Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Represents name of the current field
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Returns list of failures for the target object
        /// </summary>
        /// <returns></returns>
        public IList<Failure> GetFailures()
        {
            return this.failures;
        }

        public void SetTarget(T target, string path, ITypeValidatorBase parent)
        {
            this.target = target;
            this.path = path;
            this.parent = parent;
            this.validTarget = true;
            this.failures = null;
        }

        /// <summary>
        /// Sets current validated field value and name.
        /// if value is not NULL, it automatically marks this field as assigned (even if it was not present in JSON)I
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <param name="value">field value</param>
        /// <returns></returns>
        public void Field(string name, object value)
        {
            this.value = value;
            this.name = name;
            this.stopped = false;
            this.validField = true;
            if (null != value)
            {
                this.target.AddAssignedField(name);
            }
        }

        /// <summary>
        /// Sets current validated field name and value as NULL
        /// </summary>
        /// <param name="name">name of the field</param>
        /// <returns></returns>
        public void Field(string name)
        {
            this.value = null;
            this.name = name;
            this.stopped = false;
            this.validField = true;
        }

        /// <summary>
        /// Disables validation for the current field if it is allready failed
        /// </summary>
        /// <returns></returns>
        public void Stop()
        {
            if(!this.validField)
            {
                this.stopped = true;
            }
        }

        /// <summary>
        /// Triggers validation on nested object against specific group
        /// </summary>
        /// <typeparam name="R">Type of the validated object</typeparam>
        /// <param name="group">validation group</param>
        /// <returns></returns>
        public void Nested<R>(int group, object state)
            where R : Dtobase
        {
            if (this.stopped) return;

            TypeValidatorBase<R> nestedValidator = UpidaContext.Current().BuildValidator<R>(group);
            if (null != nestedValidator)
            {
                string fullPath = string.Concat(this.path, this.name, ".");
                nestedValidator.SetTarget((this.value as R), fullPath, this);
                nestedValidator.Validate(state);
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(R).Name + ", group:" + group);
            }
        }

        /// <summary>
        /// Triggers validation on each object from the nested collection of objects against specific group
        /// </summary>
        /// <typeparam name="R">Type of the validated object</typeparam>
        /// <param name="group">validation group</param>
        /// <returns></returns>
        public void NestedList<R>(object group, object state)
            where R : Dtobase
        {
            if (this.stopped) return;

            TypeValidatorBase<R> nestedValidator = UpidaContext.Current().BuildValidator<R>(group);
            if (null != nestedValidator)
            {
                int index = 0;
                foreach (R nestedTarget in (this.value as IEnumerable))
                {
                    string fullPath = string.Concat(this.path, this.name, "[", index, "].");
                    nestedValidator.SetTarget(nestedTarget, fullPath, this);
                    nestedValidator.Validate(state);
                    index++;
                }
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(R).Name + ", group:" + group);
            }
        }

        /// <summary>
        /// Registers a failure against the current property.
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

        public abstract void Validate(object state);
    }
}