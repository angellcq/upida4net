using System;

namespace Upida.Validation.Impl
{
    /// <summary>
    /// Defines arithmetic operations.
    /// All methods return 'false' if one of the parameters is NULL, otherwise arithmetic result is returned
    /// </summary>
    public class Math : IMath
    {
        #region long
        public bool IsLess(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(long? a, long? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion long

        #region ulong
        public bool IsLess(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(ulong? a, ulong? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion ulong

        #region int
        public bool IsLess(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(int? a, int? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion int

        #region uint
        public bool IsLess(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(uint? a, uint? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion uint

        #region short
        public bool IsLess(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(short? a, short? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion short

        #region ushort
        public bool IsLess(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(ushort? a, ushort? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion ushort

        #region byte
        public bool IsLess(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(byte? a, byte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion byte

        #region sbyte
        public bool IsLess(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
        }

        public bool IsEqual(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value == b.Value;
        }

        public bool IsNotEqual(sbyte? a, sbyte? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value != b.Value;
        }
        #endregion sbyte

        #region double
        public bool IsLess(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) < System.Math.Round(b.Value);
        }

        public bool IsGreater(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) > System.Math.Round(b.Value);
        }

        public bool IsLessOrEqual(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) <= System.Math.Round(b.Value);
        }

        public bool IsGreaterOrEqual(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) >= System.Math.Round(b.Value);
        }

        public bool IsEqual(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) == System.Math.Round(b.Value, decimals);
        }

        public bool IsNotEqual(double? a, double? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) != System.Math.Round(b.Value, decimals);
        }
        #endregion double

        #region decimal
        public bool IsLess(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) < System.Math.Round(b.Value);
        }

        public bool IsGreater(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) > System.Math.Round(b.Value);
        }

        public bool IsLessOrEqual(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) <= System.Math.Round(b.Value);
        }

        public bool IsGreaterOrEqual(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) >= System.Math.Round(b.Value);
        }

        public bool IsEqual(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) == System.Math.Round(b.Value, decimals);
        }

        public bool IsNotEqual(decimal? a, decimal? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) != System.Math.Round(b.Value, decimals);
        }
        #endregion decimal

        #region float
        public bool IsLess(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) < System.Math.Round(b.Value);
        }

        public bool IsGreater(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) > System.Math.Round(b.Value);
        }

        public bool IsLessOrEqual(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) <= System.Math.Round(b.Value);
        }

        public bool IsGreaterOrEqual(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value) >= System.Math.Round(b.Value);
        }

        public bool IsEqual(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) == System.Math.Round(b.Value, decimals);
        }

        public bool IsNotEqual(float? a, float? b, int decimals)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return System.Math.Round(a.Value, decimals) != System.Math.Round(b.Value, decimals);
        }
        #endregion float
    }
}