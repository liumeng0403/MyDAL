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

    }
}
