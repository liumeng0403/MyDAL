using System;

namespace Yunyong.DataExchange.Extensions
{
    public static class ObjectMethodExtensions
    {

        /// <summary>
        ///  obj --> datetime -->str
        /// </summary>
        public static string ToDatetimeStr(this object obj)
        {
            var result = string.Empty;
            try
            {
                result = Convert.ToDateTime(obj).ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            }
            catch (Exception ex)
            {
                throw new Exception("string ToDatetimeStr(this object obj)", ex);
            }
            return result;
        }

        public static bool ToBool(this object obj)
        {
            var result = false;
            try
            {
                result = Convert.ToBoolean(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("bool ToBool(this object obj) -- error", ex);
            }
            return result;
        }

    }
}
