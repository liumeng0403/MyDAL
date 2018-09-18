using System;

namespace Yunyong.DataExchange.Extensions
{
    public static class StringMethodExtensions
    {

        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this string str)  // LM
        {
            bool result = false;
            try
            {
                result = string.IsNullOrWhiteSpace(str);
            }
            catch (Exception ex)
            {
                throw new Exception("bool IsNullStr(this string str) -- error", ex);
            }
            return result;
        }

        /************************************************************************************************************************************************/
        
        public static bool ToBool(this string str)
        {
            bool result = false;
            try
            {
                result = Convert.ToBoolean(str);
            }
            catch(Exception ex)
            {
                throw new Exception("bool ToBool(this string str) -- error", ex);
            }
            return result;
        }
        public static short ToShort(this string str)
        {
            var result = default(short);
            try
            {
                result = Convert.ToInt16(str);
            }
            catch (Exception ex)
            {
                throw new Exception("short ToShort(this string str) -- error", ex);
            }
            return result;
        }
        public static int ToInt(this string str)
        {
            var result = default(int);
            try
            {
                result = Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                throw new Exception("int ToInt(this string str) -- error", ex);
            }
            return result;
        }
        public static long ToLong(this string str)
        {
            var result = default(long);
            try
            {
                result = Convert.ToInt64(str);
            }
            catch (Exception ex)
            {
                throw new Exception("long ToLong(this string str) -- error", ex);
            }
            return result;
        }
    }
}
