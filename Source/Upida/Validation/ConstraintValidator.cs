using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Upida.Validation
{
	/// <summary>
	/// Represents standard set of validation routines
	/// </summary>
	/// <typeparam name="T">validated type</typeparam>
	public abstract class ConstraintValidator<T> : ValidatorBase<T>
		where T : Dtobase
	{
		/// <summary>
		/// Checks if field value is assigned
		/// </summary>
		/// <returns></returns>
		public virtual bool IsAssigned()
		{
			return this.Target.IsFieldAssigned(this.Name);
		}

		/// <summary>
		/// Validates if field is assigned some value by parser. Stops if failed.
		/// </summary>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeAssigned(string msg)
		{
			if (this.Stopped) return;

			if (!this.IsAssigned())
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
		public virtual void MustBeNotAssigned(string msg)
		{
			if (this.Stopped) return;

			if (this.IsAssigned())
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value was correctly parsed
		/// </summary>
		/// <returns></returns>
		public virtual bool IsValidFormat()
		{
			return !this.Target.IsFieldWrong(this.Name);
		}

		/// <summary>
		/// Validates if field is correctly parsed. Stops if failed.
		/// </summary>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeValidFormat(string msg)
		{
			if (this.Stopped) return;

			if (!this.IsValidFormat())
			{
				this.Fail(msg);
				this.Stop();
			}
		}

		/// <summary>
		/// Checks if field value is NULL
		/// </summary>
		/// <returns></returns>
		public virtual bool IsNull()
		{
			return null == this.Value;
		}

		/// <summary>
		/// Validates if field value is null
		/// </summary>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeNull(string msg)
		{
			if (this.Stopped) return;

			if (!this.IsNull())
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Validates if field value is not null. Stops if failed.
		/// </summary>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeNotNull(string msg)
		{
			if (this.Stopped) return;

			if (this.IsNull())
			{
				this.Fail(msg);
				this.Stop();
			}
		}

		/// <summary>
		/// Checks if field value is equal to something
		/// </summary>
		/// <returns></returns>
		public virtual bool IsEqualTo(object value)
		{
			return object.Equals(value, this.Value);
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

			if (!this.IsEqualTo(value))
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
		public virtual void MustNotEqualTo(object value, string msg)
		{
			if (this.Stopped) return;

			if (this.IsEqualTo(value))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is equal to one of values
		/// </summary>
		/// <returns></returns>
		public virtual bool IsEqualToOneOf(object[] values)
		{
			bool success = false;
			for (int i = 0; i < values.Length; i++)
			{
				if (object.Equals(values[i], this.Value))
				{
					success = true;
					break;
				}
			}

			return success;
		}

		/// <summary>
		/// Validates if field value is equal to destination value
		/// </summary>
		/// <param name="value">destination value</param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustEqualToOneOf(object[] values, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsEqualToOneOf(values))
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
		public virtual void MustNotEqualToAnyOf(object[] values, string msg)
		{
			if (this.Stopped) return;

			if (this.IsEqualToOneOf(values))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is empty string
		/// </summary>
		/// <returns></returns>
		public virtual bool IsEmptyString()
		{
			return string.Equals(string.Empty, this.Value);
		}

		/// <summary>
		/// Validates field value is not empty - (for string only)
		/// </summary>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeNotEmptyString(string msg)
		{
			if (this.Stopped) return;

			if (this.IsEmptyString())
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value length is between min and max values
		/// </summary>
		/// <returns></returns>
		public virtual bool IsLengthBetween(int min, int max)
		{
			string val = (string)this.Value;
			return val.Length >= min && val.Length <= max;
		}

		/// <summary>
		/// Validates String length is between min and max values
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustHaveLengthBetween(int min, int max, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsLengthBetween(min, max))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is less than or equal to 'm'
		/// </summary>
		/// <returns></returns>
		public virtual bool IsLessOrEqualTo(object m)
		{
			return ((IComparable)this.Value).CompareTo(m) <= 0;
		}

		/// <summary>
		/// Validates field value is less than or equal to 'm'
		/// </summary>
		/// <param name="m"></param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeLessOrEqualTo(object m, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsLessOrEqualTo(m))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is less than 'm'. IComparable.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsLessThan(object m)
		{
			return ((IComparable)this.Value).CompareTo(m) < 0;
		}

		/// <summary>
		/// Validates field value is less than 'm'. IComparable.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeLessThan(object m, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsLessThan(m))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is greater than or equal to 'm'. IComparable.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsGreaterOrEqualTo(object m)
		{
			return ((IComparable)this.Value).CompareTo(m) >= 0;
		}

		/// <summary>
		/// Validates field value is greater than or equal to 'm'. IComparable.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeGreaterOrEqualTo(object m, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsGreaterOrEqualTo(m))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value is greater than 'm'. IComparable.
		/// </summary>
		/// <returns></returns>
		public virtual bool IsGreaterThan(object m)
		{
			return ((IComparable)this.Value).CompareTo(m) > 0;
		}

		/// <summary>
		/// Validates field value is greater than 'm'. IComparable.
		/// </summary>
		/// <param name="m"></param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustBeGreaterThan(object m, String msg)
		{
			if (this.Stopped) return;

			if (!this.IsGreaterThan(m))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value collection size is between min and max values inclusively (field must implement ICollection)
		/// </summary>
		/// <returns></returns>
		public virtual bool IsCountBetween(int min, int max)
		{
			ICollection collection = (ICollection)this.Value;
			return collection.Count >= min && collection.Count <= max;
		}

		/// <summary>
		/// Validates collection size is between min and max values inclusively (field must implement ICollection)
		/// </summary>
		/// <param name="min">min value</param>
		/// <param name="max">max value</param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustHaveCountBetween(int min, int max, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsCountBetween(min, max))
			{
				this.Fail(msg);
			}
		}

		/// <summary>
		/// Checks if field value qualifies to regular expression
		/// </summary>
		/// <returns></returns>
		public virtual bool IsRegexpr(string expr)
		{
			Match match = Regex.Match((string)this.Value, expr, RegexOptions.IgnoreCase);
			return match.Success;
		}

		/// <summary>
		/// Validates if field qualifies to regular expression
		/// </summary>
		/// <param name="expr">regular expression</param>
		/// <param name="msg">failure message</param>
		/// <returns></returns>
		public virtual void MustRegexpr(string expr, string msg)
		{
			if (this.Stopped) return;

			if (!this.IsRegexpr(expr))
			{
				this.Fail(msg);
			}
		}
	}
}