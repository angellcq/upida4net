using System;
using System.Collections;
using System.Collections.Generic;

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
        }

        protected ValidatorBase<T> Field(Object value, String name)
        {
            this.value = value;
            this.name = name;
            this.stopped = false;
            this.validField = true;
            return this;
        }

        public ValidatorBase<T> Stop()
        {
            if(!this.validField)
            {
                this.stopped = true;
            }
            return this;
        }

        public ValidatorBase<T> MustBeAssigned(String msg)
        {
            if(this.stopped) return this;

            if(!this.target.isFieldAssigned(this.name))
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> MustBeUnassigned(String msg)
        {
            if(this.stopped) return this;

            if(this.target.isFieldAssigned(this.name))
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> ValidFormat(String msg)
        {
            if(this.stopped) return this;

            if(this.target.isFieldWrong(this.name))
            {
                this.Fail(msg);
            }

            return this;
        }

        public ValidatorBase<T> MustBeNull(String msg)
        {
            if(this.stopped) return this;

            if(null != this.value)
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> MustEqualTo(T value, String msg)
        {
            if(this.stopped) return this;

            if(!value.Equals(this.value))
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> NotEqualTo(T value, String msg)
        {
            if(this.stopped) return this;

            if(value.Equals(this.value))
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> NotNull(String msg)
        {
            if(this.stopped) return this;

            if(null == this.value)
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> Length(int min, int max, String msg)
        {
            if(this.stopped) return this;

            String val = (String)this.value;
            if(val.Length < min || val.Length > max)
            {
                this.Fail(msg);
            }

            return this;
        }

        public ValidatorBase<T> LessOrEqualTo(Object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if(((IComparable)this.value).CompareTo(m) > 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> LessThan(Object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if (((IComparable)this.value).CompareTo(m) >= 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> GreaterOrEqualTo(Object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if (((IComparable)this.value).CompareTo(m) < 0)
            {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> GreaterThan(Object m, String msg)
        {
            if(this.stopped || null == m) return this;

            if(((IComparable)this.value).CompareTo(m) <= 0) {
                this.Fail(msg);
            }
            return this;
        }

        public ValidatorBase<T> Size(int min, int max, String msg)
        {
            if(this.stopped) return this;

            ICollection collection = (ICollection)this.value;
            if (collection.Count < min || collection.Count > max)
            {
                this.Fail(msg);
            }
            return this;
        }

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

        public bool IsValid
        {
            get { return this.validTarget; }
        }

        public void Fail(string msg)
        {
            this.Fail(new Failure(string.Concat(this.path, this.name), msg));
            this.validField = false;
        }

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