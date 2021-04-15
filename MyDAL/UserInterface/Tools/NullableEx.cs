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
    }
}
