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
		IList<Failure> Validate<T>(T target, object group) where T : Dtobase;

		/// <summary>
		/// Validates target and returns list of failures or Null
		/// </summary>
		/// <typeparam name="T">type of object to validate</typeparam>
		/// <param name="target">object to validate</param>
		/// <param name="group">validation group</param>
		/// <param name="state">optional state data</param>
		/// <returns></returns>
		IList<Failure> Validate<T>(T target, object group, object state) where T : Dtobase;

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
	}
}