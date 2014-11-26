using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upida.Validation;

namespace MyClients.Validation.Impl
{
    public class BasicValidator : Validator
    {
        public void Required()
        {
            this.MustBeAssigned("required");
            if (this.IsFieldValid && this.IsValidFormat())
            {
                this.MustBeNotNull("required");
            }
        }

        public void Email(string msg)
        {
            const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
            this.MustRegexpr(expr, msg);
        }

        public void Missing()
        {
            this.MustBeNotAssigned("must be empty");
        }

        public void NameLength()
        {
            this.MustHaveLengthBetween(3, 50, "must be between 3 and 50 characters");
        }

        public void MinimumFilterLength()
        {
            this.MustHaveLengthBetween(3, 100, "must have at least 3 characters");
        }

        public void OnlyCharsAndDigits()
        {
            this.MustRegexpr(@"^[a-zA-Z0-9]+$", "only characters and digits");
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

        public void TrueFalse()
        {
            this.MustBeValidFormat("must be 'yes' or 'no'");
        }

        public void ValidFormat()
        {
            this.MustBeValidFormat("wrong format");
        }

        public void GreaterOrEqualToZero()
        {
            this.MustBeGreaterOrEqualTo((long)0, "must be greater or equal to 0");
        }

        public void GreaterThanZero()
        {
            this.MustBeGreaterThan((long)0, "must be greater than 0");
        }
    }
}