using Yunyong.DataExchange.AdoNet;
using System;
using System.Data;

namespace Yunyong.DataExchange.Cache
{
    internal class CacheInfo
    {
        public DeserializerState Deserializer { get; set; }

        public Action<IDbCommand, DynamicParameters> ParamReader { get; set; }

    }
}
