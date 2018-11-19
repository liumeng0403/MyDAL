using System;

namespace Yunyong.DataExchange.Core.Extensions
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

        internal static bool IsSingleColumn(this Type type)
        {
            if (type.IsValueType
                || type == XConfig.TC.String)
            {
                return true;
            }
            return false;
        }
    }
}
