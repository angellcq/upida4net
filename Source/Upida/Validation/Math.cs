using System;

namespace Upida.Validation
{
    public class Math : IMath
    {
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



        public bool IsLess(double? a, double? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(double? a, double? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(double? a, double? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(double? a, double? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
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



        public bool IsLess(decimal? a, decimal? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value < b.Value;
        }

        public bool IsGreater(decimal? a, decimal? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value > b.Value;
        }

        public bool IsLessOrEqual(decimal? a, decimal? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value <= b.Value;
        }

        public bool IsGreaterOrEqual(decimal? a, decimal? b)
        {
            if (!a.HasValue || !b.HasValue) return false;
            return a.Value >= b.Value;
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
    }
}