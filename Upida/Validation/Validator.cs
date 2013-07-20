using System;

namespace Upida.Validation
{
    public class Validator
    {
        public void Validate<T>(T target, object group)
            where T : Dtobase
        {
            ValidatorBase<T> validator = FluentAttribute.BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate();

                if (!validator.IsValid)
                {
                    throw new ValidationException(validator.GetFailures());
                }
            }
        }
    }
}