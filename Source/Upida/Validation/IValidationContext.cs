using System.Collections.Generic;

namespace Upida.Validation
{
	public interface IValidationContext
	{
		/// <summary>
		/// Validates target and returns list of failures or Null
		/// </summary>
		/// <typeparam name="T">type of object to validate</typeparam>
		/// <param name="target">object to validate</param>
		/// <param name="group">validation group</param>
		/// <returns></returns>
		IFailureList Validate<T>(T target, object group) where T : Dtobase;

		/// <summary>
		/// Validates target and returns list of failures or Null
		/// </summary>
		/// <typeparam name="T">type of object to validate</typeparam>
		/// <param name="target">object to validate</param>
		/// <param name="group">validation group</param>
		/// <param name="state">optional state data</param>
		/// <returns></returns>
		IFailureList Validate<T>(T target, object group, object state) where T : Dtobase;

		/// <summary>
		/// Validates target and throws ValidationException with list of failures
		/// </summary>
		/// <typeparam name="T">type of object to validate</typeparam>
		/// <param name="target">object to validate</param>
		/// <param name="group">validation group</param>
		void AssertValid<T>(T target, object group) where T : Dtobase;

		/// <summary>
		/// Validates target and throws ValidationException with list of failures
		/// </summary>
		/// <typeparam name="T">type of object to validate</typeparam>
		/// <param name="target">object to validate</param>
		/// <param name="group">validation group</param>
		/// <param name="state">optional state data</param>
		void AssertValid<T>(T target, object group, object state) where T : Dtobase;

		/// <summary>
		/// Creates empty list of failures.
		/// Method is used together with AssertIsEmpty() method for custom failures.
		/// Create list of failures and, based on this list, throw ValidationException, using the AssertIsEmpty() method.
		/// </summary>
		/// <returns>empty list of failures</returns>
		IFailureList CreateFailureList();

		/// <summary>
		/// Throws ValidationException if list of failures is not empty.
		/// This method is usually used together with CreateFailureList() method for custom failures.
		/// When creating a custom type validator with group is not an option, AssertIsEmpty is used,
		/// as the ValidationException can be thrown based on a custom list of Failures.
		/// Create list of failures and, based on this list, throw ValidationException, using the AssertIsEmpty() method.
		/// </summary>
		/// <param name="errors">list of failures</param>
		void Assert(IFailureList errors);
	}
}