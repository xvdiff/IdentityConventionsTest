using System;

namespace IdentityConventionsTest.Core
{
	public static class StringExtensions
	{
		public static bool IsNullOrEmpty(this string source)
		{
			return String.IsNullOrEmpty(source);
		}

		public static bool IsNullOrWhitespace(this string source)
		{
			return String.IsNullOrWhiteSpace(source);
		}

		public static string Args(this string @string, params object[] args)
		{
			return String.Format(@string, args);
		}
	}
}
