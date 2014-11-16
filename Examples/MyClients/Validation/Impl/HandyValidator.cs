using Upida;
using Upida.Validation;

namespace MyClients.Validation.Impl
{
    public class HandyValidator : Validator, IHandyValidator
    {
        public bool IsAssignedAndNotNull()
        {
            return this.IsAssigned() && this.fieldValue != null;
        }

        public void Required()
        {
            this.MustBeAssigned(Errors.REQUIRED);
            this.MustBeNotNull(Errors.REQUIRED);
        }

        public void Required(string wrongFormatMessage)
        {
            this.MustBeAssigned(Errors.REQUIRED);
            this.MustBeValidFormat(wrongFormatMessage);
            this.MustBeNotNull(Errors.REQUIRED);
        }

        public void RequiredIfAssigned()
        {
            if (this.IsAssignedAndNotNull())
            {
                this.Required(Errors.MUST_BE_NUMBER);
            }
        }

        public void MustBeEmail(string msg)
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            this.MustRegexpr(expr, msg);
        }

        public void MissingField(string field, object value)
        {
            this.SetField(field, value);
            this.SetSeverity(Severity.Fatal);
            this.MustBeNotAssigned(Errors.MUST_BE_EMPTY);
        }
    }
}