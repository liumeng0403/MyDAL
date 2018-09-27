namespace Yunyong.DataExchange.Core
{
    public class Configs
    {
        public static int CommandTimeout { get; set; } = 10;  // 10s 

        /// <summary>
        /// Default is 4000, any value larger than this field will not have the default value applied.
        /// </summary>
        internal static int StringDefaultLength { get; } = 4000;
    }
}
