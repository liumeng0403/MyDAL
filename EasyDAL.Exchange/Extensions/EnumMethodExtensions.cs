using System;
using System.ComponentModel;
using System.Reflection;

namespace EasyDAL.Exchange.Extensions
{
    public static class EnumMethodExtensions
    {
        /*
         * LM , 2013
         */
        /// <summary>
        /// EnumValue(short) ---> EnumDescription(string)
        /// </summary>
        public static string ToEnumDesc<TEnum>(this short enumValue)  // LM
            where TEnum : struct
        {
            return ToEnumDescription<TEnum>(enumValue.ToString());
        }

        /// <summary>
        /// EnumValue(int) ---> EnumDescription(string)
        /// </summary>
        public static string ToEnumDesc<TEnum>(this int enumValue)   // LM
            where TEnum : struct
        {
            return ToEnumDescription<TEnum>(enumValue.ToString());
        }

        /// <summary>
        /// EnumValue(enum) ---> EnumDescription(string)
        /// </summary>
        public static string ToEnumDesc<TEnum>(this ValueType enumValue)  // LM
            where TEnum : struct
        {
            return ToEnumDescription<TEnum>(enumValue.ToString());
        }

        /// <summary>
        /// EnumValue(string) ---> EnumDescription(string)
        /// </summary>
        public static string ToEnumDesc<TEnum>(this string enumValue) // LM
            where TEnum : struct
        {
            return ToEnumDescription<TEnum>(enumValue.Trim());
        }

        /// <summary>
        /// 公用
        /// </summary>
        private static string ToEnumDescription<TEnum>(string enumValue)     // LM
            where TEnum : struct
        {
            var result = string.Empty;
            try
            {
                var enumName = ((TEnum)Enum.Parse(typeof(TEnum), enumValue)).ToString();
                var enumMember = typeof(TEnum).GetMember(enumName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)[0];
                var enumDescAttr = enumMember.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute;
                result = enumDescAttr.Description;
            }
            catch (Exception ex)
            { }
            return result;
        }

        /************************************************************************************************************************************************/

        /// <summary>
        /// EnumValue(short) ---> Enum(TEnum)
        /// </summary>
        public static TEnum ToEnum<TEnum>(this short enumValueShort)   // LM
            where TEnum : struct
        {
            return ToEnumType<TEnum>(enumValueShort.ToString());
        }

        /// <summary>
        /// EnumValue(int) ---> Enum(TEnum)
        /// </summary>
        public static TEnum ToEnum<TEnum>(this int enumValue)   // LM
            where TEnum : struct
        {
            return ToEnumType<TEnum>(enumValue.ToString());
        }

        /// <summary>
        /// EnumValue(string) ---> Enum(TEnum)
        /// </summary>
        public static TEnum ToEnum<TEnum>(this string enumValueString)   // LM
            where TEnum : struct
        {
            return ToEnumType<TEnum>(enumValueString.Trim());
        }

        /// <summary>
        /// 公用
        /// </summary>
        private static TEnum ToEnumType<TEnum>(string enumValue)   // LM
            where TEnum : struct
        {
            var result = default(TEnum);
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), enumValue, true);
            }
            catch (Exception ex)
            { }
            return result;
        }

    }
}
