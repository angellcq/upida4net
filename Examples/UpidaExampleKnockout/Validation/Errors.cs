using System;

namespace UpidaExampleKnockout.Validation
{
	public class Errors
	{
		public const string MUST_BE_EMPTY = "must be empty";
		public const string REQUIRED = "is required";
		public const string LENGTH_2_AND_50 = "must be between 2 and 50 characters";
		public const string LENGTH_5_AND_50 = "must be between 5 and 50 characters";
		public const string LENGTH_ZIP = "must be 5 characters";
		public const string GREATER_ZERO = "must be greater than zero";
		public const string WRONG_FORMAT = "wrong format";
		public const string WRONG_COUNT = "is wrong count";
		public const string EMAIL = "must be valid email";
		public const string MUST_BE_MONEY = "must be numeric amount of money";
		public const string MUST_BE_ID = "must be numeric ID value";
		public const string MUST_BE_NUMBER = "must be number";
	}
}