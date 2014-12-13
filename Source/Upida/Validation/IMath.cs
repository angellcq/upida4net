using System;

namespace Upida.Validation
{
    /// <summary>
    /// Defines arithmetic operations.
    /// All methods return 'false' if one of the parameters is NULL, otherwise arithmetic result is returned
    /// </summary>
    public interface IMath
    {
        bool IsLess(long? a, long? b);
        bool IsGreater(long? a, long? b);
        bool IsLessOrEqual(long? a, long? b);
        bool IsGreaterOrEqual(long? a, long? b);
        bool IsEqual(long? a, long? b);
        bool IsNotEqual(long? a, long? b);

        bool IsLess(ulong? a, ulong? b);
        bool IsGreater(ulong? a, ulong? b);
        bool IsLessOrEqual(ulong? a, ulong? b);
        bool IsGreaterOrEqual(ulong? a, ulong? b);
        bool IsEqual(ulong? a, ulong? b);
        bool IsNotEqual(ulong? a, ulong? b);

        bool IsLess(int? a, int? b);
        bool IsGreater(int? a, int? b);
        bool IsLessOrEqual(int? a, int? b);
        bool IsGreaterOrEqual(int? a, int? b);
        bool IsEqual(int? a, int? b);
        bool IsNotEqual(int? a, int? b);

        bool IsLess(uint? a, uint? b);
        bool IsGreater(uint? a, uint? b);
        bool IsLessOrEqual(uint? a, uint? b);
        bool IsGreaterOrEqual(uint? a, uint? b);
        bool IsEqual(uint? a, uint? b);
        bool IsNotEqual(uint? a, uint? b);

        bool IsLess(short? a, short? b);
        bool IsGreater(short? a, short? b);
        bool IsLessOrEqual(short? a, short? b);
        bool IsGreaterOrEqual(short? a, short? b);
        bool IsEqual(short? a, short? b);
        bool IsNotEqual(short? a, short? b);

        bool IsLess(ushort? a, ushort? b);
        bool IsGreater(ushort? a, ushort? b);
        bool IsLessOrEqual(ushort? a, ushort? b);
        bool IsGreaterOrEqual(ushort? a, ushort? b);
        bool IsEqual(ushort? a, ushort? b);
        bool IsNotEqual(ushort? a, ushort? b);

        bool IsLess(byte? a, byte? b);
        bool IsGreater(byte? a, byte? b);
        bool IsLessOrEqual(byte? a, byte? b);
        bool IsGreaterOrEqual(byte? a, byte? b);
        bool IsEqual(byte? a, byte? b);
        bool IsNotEqual(byte? a, byte? b);

        bool IsLess(sbyte? a, sbyte? b);
        bool IsGreater(sbyte? a, sbyte? b);
        bool IsLessOrEqual(sbyte? a, sbyte? b);
        bool IsGreaterOrEqual(sbyte? a, sbyte? b);
        bool IsEqual(sbyte? a, sbyte? b);
        bool IsNotEqual(sbyte? a, sbyte? b);

        bool IsLess(double? a, double? b, int decimals);
        bool IsGreater(double? a, double? b, int decimals);
        bool IsLessOrEqual(double? a, double? b, int decimals);
        bool IsGreaterOrEqual(double? a, double? b, int decimals);
        bool IsEqual(double? a, double? b, int decimals);
        bool IsNotEqual(double? a, double? b, int decimals);

        bool IsLess(decimal? a, decimal? b, int decimals);
        bool IsGreater(decimal? a, decimal? b, int decimals);
        bool IsLessOrEqual(decimal? a, decimal? b, int decimals);
        bool IsGreaterOrEqual(decimal? a, decimal? b, int decimals);
        bool IsEqual(decimal? a, decimal? b, int decimals);
        bool IsNotEqual(decimal? a, decimal? b, int decimals);

        bool IsLess(float? a, float? b, int decimals);
        bool IsGreater(float? a, float? b, int decimals);
        bool IsLessOrEqual(float? a, float? b, int decimals);
        bool IsGreaterOrEqual(float? a, float? b, int decimals);
        bool IsEqual(float? a, float? b, int decimals);
        bool IsNotEqual(float? a, float? b, int decimals);
    }
}