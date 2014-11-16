using System;

namespace Upida.Validation
{
    public class PathHelper
    {
        public string BuildPath(IValidator validator)
        {
            if (null == validator) return string.Empty;

            if (validator.Index.HasValue)
            {
                return string.Concat(
                    validator.Path,
                    "[",
                    validator.Index.Value,
                    "]",
                    ".",
                    validator.FieldName);
            }
            else
            {
                return string.Concat(
                    validator.Path,
                    ".",
                    validator.FieldName);
            }
        }

        public string BuildPath(IValidator validator, string nestedField)
        {
            return string.Concat(this.BuildPath(validator), ".", nestedField);
        }

        public string BuildPath(IValidator validator, int index, string nestedField)
        {
            return string.Concat(this.BuildPath(validator), "[", index, "].", nestedField);
        }
    }
}