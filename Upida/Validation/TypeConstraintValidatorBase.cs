using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
    public abstract class TypeConstraintValidatorBase<T> : TypeValidatorBase<T>
        where T : Dtobase
    {
        /// <summary>
        /// Validates if field is assigned some value by parser and calls Stop on failure
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void MustBeAssigned(string msg)
        {
            if (this.Stopped) return;

            if (!this.Target.IsFieldAssigned(this.Name))
            {
                this.Fail(msg);
                this.Stop();
            }
        }

        /// <summary>
        /// Validates if field is clean - never assigned any value by parser
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void NotAssigned(string msg)
        {
            if (this.Stopped) return;

            if (this.Target.IsFieldAssigned(this.Name))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field is correctly parsed and calls Stop on failure
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void ValidFormat(string msg)
        {
            if (this.Stopped) return;

            if (this.Target.IsFieldWrong(this.Name))
            {
                this.Fail(msg);
                this.Stop();
            }
        }

        /// <summary>
        /// Validates if field value is null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void MustBeNull(string msg)
        {
            if (this.Stopped) return;

            if (null != this.Value)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field value is equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void MustEqualTo(object value, string msg)
        {
            if (this.Stopped) return;

            if (!value.Equals(this.Value))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value not equal to destination value
        /// </summary>
        /// <param name="value">destination value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void NotEqualTo(object value, string msg)
        {
            if (this.Stopped) return;

            if (value.Equals(this.Value))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is not null
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void NotNull(string msg)
        {
            if (this.Stopped) return;

            if (null == this.Value)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is not empty - (for string only)
        /// </summary>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void NotEmpty(string msg)
        {
            if (this.Stopped) return;

            if (string.Equals(string.Empty, this.Value))
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates String length is between min and max values
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void Length(int min, int max, string msg)
        {
            if (this.Stopped) return;

            string val = (string)this.Value;
            if (val.Length < min || val.Length > max)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void LessOrEqualTo(object m, String msg)
        {
            if (this.Stopped) return;

            if (((IComparable)this.Value).CompareTo(m) > 0)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is less than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void LessThan(object m, string msg)
        {
            if (this.Stopped) return;

            if (((IComparable)this.Value).CompareTo(m) >= 0)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than or equal to 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void GreaterOrEqualTo(object m, string msg)
        {
            if (this.Stopped) return;

            if (((IComparable)this.Value).CompareTo(m) < 0)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates field value is greater than 'm'
        /// </summary>
        /// <param name="m"></param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void GreaterThan(object m, String msg)
        {
            if (this.Stopped) return;

            if (((IComparable)this.Value).CompareTo(m) <= 0)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates collection size is between min and max values inclusively (field must be a collection)
        /// </summary>
        /// <param name="min">min value</param>
        /// <param name="max">max value</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void Size(int min, int max, string msg)
        {
            if (this.Stopped) return;

            ICollection collection = (ICollection)this.Value;
            if (collection.Count < min || collection.Count > max)
            {
                this.Fail(msg);
            }
        }

        /// <summary>
        /// Validates if field qualifies to regular expression
        /// </summary>
        /// <param name="expr">regular expression</param>
        /// <param name="msg">failure message</param>
        /// <returns></returns>
        public virtual void Regexp(string expr, string msg)
        {
            if (this.Stopped) return;

            Match match = Regex.Match((string)this.Value, expr, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                this.Fail(msg);
            }
        }
    }
}