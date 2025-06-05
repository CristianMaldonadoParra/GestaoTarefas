namespace Domain.Common.Extension
{
    public static class CommonExtensions
    {
        public static bool IsAny<T>(this IEnumerable<T> obj)
        {
            return obj is not null && obj.Count() > 0;
        }
        public static bool IsNotAny<T>(this IEnumerable<T> obj)
        {
            return !obj.IsAny();
        }


        #region Defaults
        public static bool IsNotDefault(this bool value)
        {
            return value != default(bool);
        }
        public static bool IsNotDefault(this bool? value)
        {
            return value != default(bool?);
        }
        public static bool IsNotDefault(this decimal value)
        {
            return value != default(decimal);
        }
        public static bool IsNotDefault(this decimal? value)
        {
            return value != default(decimal?);
        }
        public static bool IsNotDefault(this DateTime value)
        {
            return value != default(DateTime);
        }
        public static bool IsNotDefault(this DateTime? value)
        {
            return value.IsNotNull() && value.Value != default(DateTime);
        }
        public static bool IsNotDefault(this byte value)
        {
            return !IsDefault(value);
        }
        public static bool IsNotDefault(this int value)
        {
            return !IsDefault(value);
        }
        public static bool IsNotDefault(this byte[] values)
        {
            return !IsDefault(values);
        }
        public static bool IsNotDefault(this int[] values)
        {
            return !IsDefault(values);
        }
        public static bool IsDefault(this byte[] values)
        {
            return values == default(byte[]);
        }
        public static bool IsDefault(this int[] values)
        {
            return values == default(int[]);
        }
        public static bool IsNotDefault(this byte? value)
        {
            return !IsDefault(value);
        }
        public static bool IsNotDefault(this int? value)
        {
            return !IsDefault(value);
        }
        public static bool IsDefault(this byte value)
        {
            return value == default(byte);
        }
        public static bool IsDefault(this int value)
        {
            return value == default(int);
        }
        public static bool IsDefault(this byte? value)
        {
            return value == default(byte?);
        }
        public static bool IsDefault(this int? value)
        {
            return value == default(int?);
        }
        public static bool IsDefault(this DateTime? value)
        {
            return value == default(DateTime?);
        }
        public static bool IsDefault(this DateTime value)
        {
            return value == default(DateTime);
        }
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
        #endregion

        #region Sents
        public static bool IsSent(this Guid? value)
        {
            return value != null;
        }
        public static bool IsSent(this Guid value)
        {
            return value != default(Guid);
        }
        public static bool IsSent(this short value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this Int64 value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this Int64? value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this Int16? value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this byte[] value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this byte? value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this byte value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this int value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this int[] values)
        {
            return IsNotDefault(values);
        }
        public static bool IsSent(this decimal value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this decimal? value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this string value)
        {
            
            return IsNotNullOrEmpty(value);
        }
        public static bool IsSent(this float? value)
        {
            return value.IsNotNull();
        }
        public static bool IsSent(this float value)
        {
            return value.IsNotNull();
        }
        public static bool IsSent(this int? value)
        {
            return value.IsNotNull();
        }
        public static bool IsSent(this bool value)
        {
            return value.IsNotDefault();
        }
        public static bool IsSent(this bool? value)
        {
            return value.IsNotDefault();
        }
        public static bool IsSent(this DateTime value)
        {
            return IsNotDefault(value);
        }
        public static bool IsSent(this DateTime? value)
        {
            return value.IsNotNull();
        }
        #endregion

        #region Nulls

        public static bool IsNullOrEmpaty<T>(this IEnumerable<T> obj)
        {
            return obj.IsNull() || obj.Count() == 0;

        }
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value);
        }

        #endregion
    }
}
