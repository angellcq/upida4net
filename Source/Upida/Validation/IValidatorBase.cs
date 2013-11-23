using System;
using System.Collections.Generic;

namespace Upida.Validation
{
    public interface IValidatorBase
    {
        bool IsValidField { get; }
        bool IsValid { get; }
        bool Stopped { get; }
        object Value { get; }
        string Name { get; }
        IList<Failure> GetFailures();
        void Field(string name, object value);
        void Field(string name);
        void Stop();
        void Nested<R>(int group, object state) where R : Dtobase;
        void NestedList<R>(object group, object state) where R : Dtobase;
        void Fail(string msg);
        void Fail(Failure failure);
        void Validate(object state);
    }
}