using System;
using System.Collections;
using System.Collections.Generic;

namespace Upida.Validation
{
	public abstract class ValidatorBase<T> : IValidatorBase
		where T : Dtobase
	{
		private string path;
		private string name;
		private object value;
		private IValidatorBase parent;
		private T target;

		private bool validTarget;
		private bool validField;
		private bool stopped;
		private Severity severity;
		private IFailureList failures;

		/// <summary>
		/// True if current field is valid so far
		/// </summary>
		public virtual bool IsValidField
		{
			get { return this.validField; }
		}

		/// <summary>
		/// True is the target object is valid so far
		/// </summary>
		public virtual bool IsValid
		{
			get { return this.validTarget; }
		}

		/// <summary>
		/// True if validation is stopped for the current field. You can stop validation for current field only if Stop() method is called and the field is invalid
		/// </summary>
		public virtual bool Stopped
		{
			get { return this.stopped; }
		}

		/// <summary>
		/// Represents target object
		/// </summary>
		public virtual T Target
		{
			get { return this.target; }
		}

		/// <summary>
		/// Represents value of the current field
		/// </summary>
		public virtual object Value
		{
			get { return this.value; }
		}

		/// <summary>
		/// Represents name of the current field
		/// </summary>
		public virtual string Name
		{
			get { return this.name; }
		}

		/// <summary>
		/// Returns list of failures for the target object
		/// </summary>
		/// <returns></returns>
		public virtual IFailureList GetFailures()
		{
			return this.failures ?? new FailureList();
		}

		/// <summary>
		/// Sets target validated object
		/// </summary>
		/// <param name="target">target validated object</param>
		/// <param name="path">path to this object in hierarchy (null for root)</param>
		/// <param name="parent">parent validator</param>
		public virtual void SetTarget(T target, string path, IValidatorBase parent)
		{
			this.target = target;
			this.path = path;
			this.parent = parent;
			this.validTarget = true;
			this.failures = null;
		}

		/// <summary>
		/// Sets severity of the next checking.
		/// Severity is reset to None after checking is done.
		/// </summary>
		/// <param name="severity"></param>
		public virtual void SetSeverity(Severity severity)
		{
			this.severity = severity;
		}

		/// <summary>
		/// Sets current validated field value and name.
		/// if value is not NULL, it automatically marks this field as assigned (even if it was not present in JSON)I
		/// </summary>
		/// <param name="name">name of the field</param>
		/// <param name="value">field value</param>
		/// <returns></returns>
		public virtual void Field(string name, object value)
		{
			this.value = value;
			this.name = name;
			this.stopped = false;
			this.validField = true;
			this.severity = Severity.None;
			if (null != value)
			{
				this.target.AddAssignedField(name);
			}
		}

		/// <summary>
		/// Sets current validated field name and value as NULL
		/// </summary>
		/// <param name="name">name of the field</param>
		/// <returns></returns>
		public virtual void Field(string name)
		{
			this.value = null;
			this.name = name;
			this.stopped = false;
			this.validField = true;
			this.severity = Severity.None;
		}

		/// <summary>
		/// Disables validation for the current field if it is allready failed
		/// </summary>
		/// <returns></returns>
		public virtual void Stop()
		{
			if (!this.validField)
			{
				this.stopped = true;
			}
		}

		/// <summary>
		/// Triggers validation on nested object against specific group
		/// </summary>
		/// <typeparam name="R">Type of the validated object</typeparam>
		/// <param name="group">validation group</param>
		/// <returns></returns>
		public virtual void Nested<R>(object group, object state)
			where R : Dtobase
		{
			if (this.stopped) return;

			ValidatorBase<R> nestedValidator = UpidaContext.Current().BuildValidator<R>(group);
			if (null != nestedValidator)
			{
				string fullPath = string.Concat(this.path, this.name, ".");
				nestedValidator.SetTarget((this.value as R), fullPath, this);
				nestedValidator.Validate(state);
			}
			else
			{
				throw new ApplicationException("TypeValidator not found. type:" + typeof(R).Name + ", group:" + group);
			}
		}

		/// <summary>
		/// Triggers validation on each object from the nested collection of objects against specific group
		/// </summary>
		/// <typeparam name="R">Type of the validated object</typeparam>
		/// <param name="group">validation group</param>
		/// <returns></returns>
		public virtual void NestedList<R>(object group, object state)
			where R : Dtobase
		{
			if (this.stopped) return;

			ValidatorBase<R> nestedValidator = UpidaContext.Current().BuildValidator<R>(group);
			if (null != nestedValidator)
			{
				int index = 0;
				foreach (R nestedTarget in (this.value as IEnumerable))
				{
					string fullPath = string.Concat(this.path, this.name, "[", index, "].");
					nestedValidator.SetTarget(nestedTarget, fullPath, this);
					nestedValidator.Validate(state);
					index++;
				}
			}
			else
			{
				throw new ApplicationException("TypeValidator not found. type:" + typeof(R).Name + ", group:" + group);
			}
		}

		/// <summary>
		/// Registers a failure against the current property.
		/// Current property is determined by the last call of the Field() method.
		/// </summary>
		/// <param name="msg">failure message</param>
		public virtual void Fail(string msg)
		{
			this.Fail(new Failure(string.Concat(this.path, this.name), msg, this.severity));
			this.validField = false;
		}

		/// <summary>
		/// Registers a failure (failure includes failure message and property path)
		/// </summary>
		/// <param name="failure">failure</param>
		public virtual void Fail(Failure failure)
		{
			if (this.stopped) return;

			this.validTarget = false;
			if (null == this.parent)
			{
				if (null == this.failures)
				{
					this.failures = new FailureList();
				}

				this.failures.Fail(failure);
			}
			else
			{
				this.parent.Fail(failure);
			}
		}

		public abstract void Validate(object state);
	}
}