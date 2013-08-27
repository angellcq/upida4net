using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Upida.Validation
{
    public interface IValidator
    {
        /// <summary>
        /// Validates target and returns list of failures or Null
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        /// <returns></returns>
        IList<Failure> Validate<T>(T target, object group) where T : Dtobase;

        /// <summary>
        /// Validates target and throws ValidationException with list of failures
        /// </summary>
        /// <typeparam name="T">type of object to validate</typeparam>
        /// <param name="target">object to validate</param>
        /// <param name="group">validation group</param>
        void AssertValid<T>(T target, object group) where T : Dtobase;

        /// <summary>
        /// Publishes failures to MVC context to enable spring tags displaying them
        /// </summary>
        /// <param name="failureList"></param>
        /// <param name="modelState"></param>
        void PublishFailures(IList<Failure> failureList, ModelStateDictionary modelState);
    }
}