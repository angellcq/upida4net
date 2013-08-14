using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Upida.Validation
{
    public class Validator : IValidator
    {
        public void ValidateAndThrow<T>(T target, object group)
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

        public bool ValidateAndPublish<T>(T target, object group, ModelStateDictionary modelState)
            where T : Dtobase
        {
            HttpRequest request = HttpContext.Current.Request;
            ValidatorBase<T> validator = FluentAttribute.BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate();

                if (!validator.IsValid)
                {
                    foreach (Failure item in validator.GetFailures())
                    {
                        ModelState fieldModelState = modelState[item.Key];
                        if (null == fieldModelState)
                        {
                            fieldModelState = new ModelState();
                            fieldModelState.Value = new ValueProviderResult(request[item.Key], request[item.Key], CultureInfo.InvariantCulture);
                            modelState.Add(item.Key, fieldModelState);
                        }

                        fieldModelState.Errors.Add(new ModelError(item.Text));
                    }

                    return false;
                }
            }

            return true;
        }
    }
}