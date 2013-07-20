using System;

namespace Upida.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class FluentAttribute : Attribute
    {
        private Type validator;
        private object group;

        public FluentAttribute(Type validator, object group)
        {
            this.validator = validator;
            this.group = group;
        }

        public Type Validator
        {
            get { return this.validator; }
        }

        public object Group
        {
            get { return this.group; }
        }

        public static ValidatorBase<T> BuildValidator<T>(object group)
            where T : Dtobase
        {
            object[] fluents = typeof(T).GetCustomAttributes(typeof(FluentAttribute), false);
            for (int i = 0; i < fluents.Length; i++)
            {
                Type validatorType = null;
                FluentAttribute fluent = (FluentAttribute)fluents[i];
                if (object.Equals(fluent.group, group))
                {
                    validatorType = fluent.Validator;
                    ValidatorBase<T> nestedValidator = (ValidatorBase<T>)Activator.CreateInstance(validatorType);
                    return nestedValidator;
                }
            }

            return null;
        }
    }
}