using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Upida.Validation
{
    public class Validator : IValidator
    {
        public IList<Failure> Validate<T>(T target, object group) where T : Dtobase
        {
           TypeValidatorBase<T> validator = UpidaContext.Current().BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate();

                if (!validator.IsValid)
                {
                    return validator.GetFailures();
                }

                return null;
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        public void AssertValid<T>(T target, object group) where T : Dtobase
        {
            TypeValidatorBase<T> validator = UpidaContext.Current().BuildValidator<T>(group);
            if (null != validator)
            {
                validator.SetTarget(target, null, null);
                validator.Validate();

                if (!validator.IsValid)
                {
                    throw new ValidationException(validator.GetFailures(), typeof(T), group);
                }
            }
            else
            {
                throw new ApplicationException("TypeValidator not found. type:" + typeof(T).Name + ", group:" + group);
            }
        }

        public void PublishFailures(IList<Failure> failureList, ModelStateDictionary modelState)
        {
            if (null != failureList)
            {
                HttpRequest request = HttpContext.Current.Request;
                foreach (Failure item in failureList)
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
            }
        }
    }
}