using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    public class ValidationContext : IValidationContext
    {
        /// <summary>
        /// Validates target and returns list of failures or Null
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        /// <returns>list of failures</returns>
        public IFailureList Validate<T>(T target, Enum group) where T : Dtobase
        {
            return this.Validate(target, group, null);
        }

        public IFailureList ValidateList<T>(IEnumerable<T> list, Enum group) where T : Dtobase
        {
            return this.ValidateList(list, group, null);
        }

        /// <summary>
        /// Validates target and returns list of failures or Null
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        /// <param name="state">optional state data</param>
        /// <returns>list of failures</returns>
        public IFailureList Validate<T>(T target, Enum group, object state) where T : Dtobase
        {
            ValidatorBase<T> validator = UpidaContext.Current.BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate(group, state);
                return validator.GetFailures();
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        public IFailureList ValidateList<T>(IEnumerable<T> list, Enum group, object state) where T : Dtobase
        {
            ValidatorBase<T> validator = UpidaContext.Current.BuildValidator<T>(group);
            if (null != validator)
            {
                int index = 0;
                foreach (T target in list)
                {
                    string fullPath = string.Concat("[", index, "].");
                    validator.SetTarget(target, fullPath, null);
                    validator.Validate(group, state);
                    index++;
                }

                return validator.GetFailures();
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        /// <summary>
        /// Validates target and throws ValidationException with list of failures
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        public void AssertValid<T>(T target, Enum group) where T : Dtobase
        {
            this.AssertValid(target, group, null);
        }

        public void AssertValidList<T>(IEnumerable<T> list, Enum group) where T : Dtobase
        {
            this.AssertValidList(list, group, null);
        }

        /// <summary>
        /// Validates target and throws ValidationException with list of failures
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        /// <param name="state">optional state data</param>
        public void AssertValid<T>(T target, Enum group, object state) where T : Dtobase
        {
            ValidatorBase<T> validator = UpidaContext.Current.BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate(group, null);

                if (!validator.IsValid)
                {
                    throw new ValidationException(validator.GetFailures(), typeof(T), group);
                }
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        public void AssertValidList<T>(IEnumerable<T> list, Enum group, object state) where T : Dtobase
        {
            ValidatorBase<T> validator = UpidaContext.Current.BuildValidator<T>(group);
            if (null != validator)
            {
                int index = 0;
                foreach (T target in list)
                {
                    string fullPath = string.Concat("[", index, "].");
                    validator.SetTarget(target, fullPath, null);
                    validator.Validate(group, state);
                    index++;
                }

                if (!validator.IsValid)
                {
                    throw new ValidationException(validator.GetFailures(), typeof(T), group);
                }
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        /// <summary>
        /// Creates empty list of failures.
        /// Method is used together with AssertIsEmpty() method for custom failures.
        /// Create list of failures and, based on this list, throw ValidationException, using the AssertIsEmpty() method.
        /// </summary>
        /// <returns>empty list of failures</returns>
        public IFailureList CreateFailureList()
        {
            return new FailureList();
        }

        /// <summary>
        /// Throws ValidationException if list of failures is not empty.
        /// This method is usually used together with CreateFailureList() method for custom failures.
        /// When creating a custom type validator with group is not an option, AssertIsEmpty is used,
        /// as the ValidationException can be thrown based on a custom list of Failures.
        /// Create list of failures and, based on this list, throw ValidationException, using the AssertIsEmpty() method.
        /// </summary>
        /// <param name="errors">list of failures</param>
        public void Assert(IFailureList errors)
        {
            if (null != errors && !errors.IsEmpty)
            {
                throw new ValidationException(errors);
            }
        }
    }
}