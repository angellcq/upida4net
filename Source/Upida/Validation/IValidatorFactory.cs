using System;

namespace Upida.Validation
{
    public interface IValidatorFactory
    {
        IValidatorBase GetInstance(Type typeValidatorType);
    }
}