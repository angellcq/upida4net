using System;

namespace Upida.Validation
{
    public interface ITypeValidatorFactory
    {
        ITypeValidatorBase GetInstance(Type typeValidatorType);
    }
}