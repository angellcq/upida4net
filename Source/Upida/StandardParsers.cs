using System;
using System.Globalization;

namespace Upida
{
	public static class StandardParsers
	{
		public static readonly IParser STRING_PARSER = new StringParser();
		public static readonly IParser LONG_PARSER = new LongParser();
		public static readonly IParser ULONG_PARSER = new ULongParser();
		public static readonly IParser INT_PARSER = new IntParser();
		public static readonly IParser UINT_PARSER = new UIntParser();
		public static readonly IParser SHORT_PARSER = new ShortParser();
		public static readonly IParser USHORT_PARSER = new UShortParser();
		public static readonly IParser BYTE_PARSER = new ByteParser();
		public static readonly IParser SBYTE_PARSER = new SByteParser();
		public static readonly IParser DOUBLE_PARSER = new DoubleParser();
		public static readonly IParser FLOAT_PARSER = new FloatParser();
		public static readonly IParser BOOL_PARSER = new BoolParser();
		public static readonly IParser CHAR_PARSER = new CharParser();
		public static readonly IParser ENUM_PARSER = new EnumParser();
		public static readonly IParser DATETIME_PARSER = new DateTimeParser();

		private class StringParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return text;
			}
		}

		private class LongParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return long.Parse(text);
			}
		}

		private class ULongParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return ulong.Parse(text);
			}
		}

		private class IntParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return int.Parse(text);
			}
		}

		private class UIntParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return uint.Parse(text);
			}
		}

		private class ShortParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return short.Parse(text);
			}
		}

		private class UShortParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return ushort.Parse(text);
			}
		}

		private class ByteParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return byte.Parse(text);
			}
		}

		private class SByteParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return sbyte.Parse(text);
			}
		}

		private class DoubleParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return double.Parse(text, CultureInfo.InvariantCulture);
			}
		}

		private class FloatParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return float.Parse(text, CultureInfo.InvariantCulture);
			}
		}

		private class BoolParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return bool.Parse(text);
			}
		}

		private class CharParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return text[0];
			}
		}

		private class EnumParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return Enum.Parse(type, text);
			}
		}

		private class DateTimeParser : IParser
		{
			public object ParseTextValue(Type type, string text)
			{
				return DateTime.FromOADate(double.Parse(text));
			}
		}
	}
}