using System;
using System.Data;
using Yunyong.DataExchange.AdoNet;

namespace Yunyong.DataExchange.Cache
{
    internal class CacheInfo
    {
        public DeserializerState Deserializer { get; set; }

        public Action<IDbCommand, DynamicParameters> ParamReader { get; set; }

    }
}
