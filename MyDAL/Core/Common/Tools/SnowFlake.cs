using System;

namespace HPC.DAL.Core.Common.Tools
{
    internal sealed class SnowFlake
    {
        private static long MachineID { get; } = 0L;
        private static long DataCenterID { get; } = 0L;
        private static long Sequence { get; set; } = 0L;

        public static long Twepoch { get; } = 0L;

        private static long MachineIdBits { get; } = 5L;
        private static long DataCenterIdBits { get; } = 5L;

        private static long SequenceBits { get; } = 12L;
        private static long MachineIdShift { get; } = SequenceBits;
        private static long DataCenterIdShift { get; } = SequenceBits + MachineIdBits;
        private static long TimestampLeftShift { get; } = SequenceBits + MachineIdBits + DataCenterIdBits;
        public static long SequenceMask { get; } = -1L ^ -1L << (int)SequenceBits;
        private static long LastTimestamp { get; set; } = -1L;

        private static object SyncRoot { get; } = new object();

        private static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
        }
        private static long GetNextTimestamp(long LastTimestamp)
        {
            long Timestamp = GetTimestamp();
            if (Timestamp <= LastTimestamp)
            {
                Timestamp = GetTimestamp();
            }
            return Timestamp;
        }

        internal long GetSerialID()
        {
            lock (SyncRoot)
            {
                long Timestamp = GetTimestamp();
                if (SnowFlake.LastTimestamp == Timestamp)
                { //同一微秒中生成ID  
                    Sequence = (Sequence + 1) & SequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限  
                    if (Sequence == 0)
                    {
                        //一微秒内产生的ID计数已达上限，等待下一微秒 
                        Timestamp = GetNextTimestamp(SnowFlake.LastTimestamp);
                    }
                }
                else
                {
                    //不同微秒生成ID  
                    Sequence = 0L;
                }
                if (Timestamp < LastTimestamp)
                {
                    throw new Exception("时间戳比上一次生成ID时时间戳还小，故异常");
                }
                SnowFlake.LastTimestamp = Timestamp; //把当前时间戳保存为最后生成ID的时间戳  
                long Id = ((Timestamp - Twepoch) << (int)TimestampLeftShift) | (DataCenterID << (int)DataCenterIdShift) | (MachineID << (int)MachineIdShift) | Sequence;
                return Id;
            }
        }

    }
}
