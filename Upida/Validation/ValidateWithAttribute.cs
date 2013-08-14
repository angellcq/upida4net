using System;

namespace Upida.Validation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public class ValidateWithAttribute : Attribute
    {
        private Type validator;
        private object group;

        /// <summary>
        /// Creates new instance of the DtoAttribute
        /// </summary>
        /// <param name="validator">validator class</param>
        /// <param name="group">validaton group (you can use Upida.Validation.Groups enumeration)</param>
        public ValidateWithAttribute(Type validator, object group)
        {
            this.validator = validator;
            this.group = group;
        }

        /// <summary>
        /// Creates new instance of the DtoAttribute
        /// </summary>
        /// <param name="validatorType">Fully qualified name of the validator class</param>
        /// <param name="group">validaton group (you can use Upida.Validation.Groups enumeration)</param>
        public ValidateWithAttribute(string validatorType, object group)
        {
            this.validator = Type.GetType(validatorType);
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
            object[] fluents = typeof(T).GetCustomAttributes(typeof(ValidateWithAttribute), false);
            for (int i = 0; i < fluents.Length; i++)
            {
                Type validatorType = null;
                ValidateWithAttribute fluent = (ValidateWithAttribute)fluents[i];
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