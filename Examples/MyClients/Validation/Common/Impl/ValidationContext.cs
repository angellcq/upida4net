using Upida;
using Upida.Impl;
using Upida.Validation.Impl;

namespace MyClients.Validation.Common.Impl
{
    public class ValidationContext : UpidaValidationContext, IValidationContext
    {
        public ValidationContext(IUpidaContext upidaContext)
            : base(upidaContext)
        {
        }

        public void Required()
        {
            this.MustBeAssigned("is required");
            if (this.IsFieldValid && this.IsValidFormat())
            {
                this.MustBeNotNull("is required");
            }
        }

        public void Missing()
        {
            this.MustBeNotAssigned("must be empty");
        }

        public void Number()
        {
            this.MustBeValidFormat("must be valid number");
        }

        public void Date()
        {
            this.MustBeValidFormat("must be valid date");
        }

        public void Float()
        {
            this.MustBeValidFormat("must be valid floating point number");
        }

        public void Email()
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            this.MustRegexpr(expr, "must be valid Email");
        }

        public void Text()
        {
            this.MustHaveLengthBetween(3, 20, "must be between 3 and 20 characters");
        }

        public void TrueFalse()
        {
            this.MustBeValidFormat("must be 'yes' or 'no'");
        }
    }
}