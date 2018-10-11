using System;

namespace Yunyong.DataExchange.Core.Extensions
{
    internal static class StringMethodExtensions
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
        
    }
}
