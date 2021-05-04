namespace MyDAL.Tools
{
    public static class NullableEx
    {


        public static bool ToBool(this bool? nullableBool)
        {
            return nullableBool.ToBool(false);
        }

        public static bool ToBool(this bool? nullableBool,bool defaultValueIfNull)
        {
            if (nullableBool.IsNull())
            {
                return defaultValueIfNull;
            }

            return nullableBool.Value;
        }

    }
}
