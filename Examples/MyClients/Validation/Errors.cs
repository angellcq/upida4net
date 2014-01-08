﻿using System;

namespace MyClients.Validation
{
	public class Errors
	{
		public const string MUST_BE_EMPTY = "must be empty";
		public const string REQUIRED = "required";
		public const string LENGTH_3_20 = "must be between 3 and 20 characters";
		public const string GREATER_ZERO = "must be greater than zero";
		public const string MUST_BE_NUMBER = "must be number";
		public const string NOT_VALID_BOOL = "must be true or false";
		public const string NUMBER_OF_LOGINS = "must be at least one login";
	}
}