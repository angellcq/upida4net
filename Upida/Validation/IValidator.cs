using System;
using System.Web.Mvc;

namespace Upida.Validation
{
    public interface IValidator
    {
        /// <summary>
        /// Validates and throws Validation exception if errors found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="group"></param>
        void ValidateAndThrow<T>(T target, object group) where T : Dtobase;

        /// <summary>
        /// Validates and publishes all failures to model state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="group"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        bool ValidateAndPublish<T>(T target, object group, ModelStateDictionary modelState) where T : Dtobase;
    }
}