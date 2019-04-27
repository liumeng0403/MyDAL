using HPC.DAL.Core.Common.Tools;

namespace HPC.DAL.Tools
{
    public class IDGenerator
    {
        /// <summary>
        /// Twitter SnowFlake ID
        /// </summary>
        public long LongID
        {
            get
            {
                return new SnowFlake().GetSerialID();
            }
        }
    }
}
