﻿using System;

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

        bool IsLess(int? a, int? b);
        bool IsGreater(int? a, int? b);
        bool IsLessOrEqual(int? a, int? b);
        bool IsGreaterOrEqual(int? a, int? b);
        bool IsEqual(int? a, int? b);
        bool IsNotEqual(int? a, int? b);

        bool IsLess(short? a, short? b);
        bool IsGreater(short? a, short? b);
        bool IsLessOrEqual(short? a, short? b);
        bool IsGreaterOrEqual(short? a, short? b);
        bool IsEqual(short? a, short? b);
        bool IsNotEqual(short? a, short? b);

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
    }
}