using System;

namespace MyDAL.Core.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsList(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.TC.ListT)
            {
                return true;
            }

            return false;
        }

        internal static bool IsNullable(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.TC.NullableT)
            {
                return true;
            }

            return false;
        }
    }
}
