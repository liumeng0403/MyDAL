using System;
using System.Collections.Generic;
using System.Text;

namespace EasyDAL.Exchange.Extensions
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
    }
}
