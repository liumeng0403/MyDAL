namespace MyDAL.Tools
{
    public static class NullableEx
    {
        public static bool IsNull(this bool? nullableBool)
        {
            if (null == nullableBool)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNotNull(this bool? nullableBool)
        {
            return !nullableBool.IsNull();
        }

        public static bool IsNull(this int? nullableInt)
        {
            if (null == nullableInt)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNotNull(this int? nullableInt)
        {
            return !nullableInt.IsNull();
        }

        public static bool IsNull(this long? nullableLong)
        {
            if (null == nullableLong)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsNotNull(this long? nullableLong)
        {
            return !nullableLong.IsNull();
        }
    }
}
