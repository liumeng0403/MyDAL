using System;

namespace HPC.DAL.Core.Extensions
{
    internal static class TypeExtensions
    {
        internal static bool IsList(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.CSTC.ListT)
            {
                return true;
            }
            return false;
        }

        internal static bool IsEnumerable(this Type type)
        {
            if (type.DeclaringType == XConfig.CSTC.EnumerableOfLinq)
            {
                return true;
            }
            return false;
        }

        internal static bool IsNullable(this Type type)
        {
            if (type.IsGenericType
                && type.GetGenericTypeDefinition() == XConfig.CSTC.NullableT)
            {
                return true;
            }
            return false;
        }

        internal static bool IsSimpleValueType(this Type type)
        {
            if (type.IsValueType
                && !type.IsEnum
                && type != XConfig.CSTC.DateTime
                && !type.IsNullable())
            {
                return true;
            }
            return false;
        }

        internal static bool IsSingleColumn(this Type type)
        {
            if (type.IsValueType
                || type == XConfig.CSTC.String
                || type == XConfig.CSTC.ByteArray)
            {
                return true;
            }
            return false;
        }
    }
}
