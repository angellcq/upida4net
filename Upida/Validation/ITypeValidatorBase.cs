using System;

namespace Upida.Validation
{
    public interface ITypeValidatorBase
    {
        void Fail(Failure failure);
        void Validate();
    }
}