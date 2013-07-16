using System;

namespace Upida.Validation
{
    public interface IValidatorBase
    {
        void Fail(Failure failure);
        void Validate();
    }
}