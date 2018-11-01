namespace Yunyong.DataExchange.Core.Extensions
{
    internal static class StringMethodExtensions
    {

        /// <summary>
        /// Is null/empty/whitespace ?
        /// </summary>
        public static bool IsNullStr(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /************************************************************************************************************************************************/

    }
}
