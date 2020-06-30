using MyDAL.Core.Common.Tools;

namespace MyDAL.Tools
{
    public class IDGenerator
    {
        /// <summary>
        /// Twitter SnowFlake ID
        /// </summary>
        public static long LongID
        {
            get
            {
                return new SnowFlake().GetSerialID();
            }
        }
    }
}
