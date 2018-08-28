using EasyDAL.Exchange.MapperX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using static EasyDAL.Exchange.AdoNet.SqlMapper;

namespace EasyDAL.Exchange.Cache
{
    internal class CacheInfo
    {
        public DeserializerState Deserializer { get; set; }
        public Func<IDataReader, object>[] OtherDeserializers { get; set; }
        public Action<IDbCommand, object> ParamReader { get; set; }
        private int hitCount;
        public int GetHitCount() { return Interlocked.CompareExchange(ref hitCount, 0, 0); }
        public void RecordHit()
        {
            Interlocked.Increment(ref hitCount);
        }
    }
}
