using System;

namespace Upida.Validation
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
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
			this.validator = Type.GetType(validatorType, true, true);
			this.group = group;
		}

		/// <summary>
		/// Type validator class
		/// </summary>
		public Type Validator
		{
			get { return this.validator; }
		}

		/// <summary>
		/// Validation group
		/// </summary>
		public object Group
		{
			get { return this.group; }
		}
	}
}