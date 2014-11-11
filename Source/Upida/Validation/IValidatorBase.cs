using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    /// <summary>
    /// Defines base type validator members
    /// </summary>
    public interface IValidatorBase
    {
        /// <summary>
        /// Registers a failure
        /// </summary>
        /// <param name="msg">failure message</param>
        void Fail(string msg);

        /// <summary>
        /// Registers a failure against the current property's child property
        /// </summary>
        /// <param name="nestedField">child property</param>
        /// <param name="msg">failure message</param>
        void FailNested(string nestedField, string msg);

        /// <summary>
        /// Registers a failure against the current indexedt property's child property ( i.e. currentProperty[index].childProperty)
        /// </summary>
        /// <param name="index">index of the current property</param>
        /// <param name="nestedField">child property</param>
        /// <param name="msg">failure message</param>
        void FailNested(int index, string nestedField, string msg);

        /// <summary>
        /// Registers a failure
        /// </summary>
        /// <param name="failure">failure object</param>
        void Fail(Failure failure);

        /// <summary>
        /// Validates current object and passes state information
        /// </summary>
        /// <param name="state">state information</param>
        /// <param name="state">validation group</param>
        void Validate(Enum group, object state);
    }
}