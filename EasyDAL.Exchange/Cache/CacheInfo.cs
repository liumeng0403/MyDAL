using MyDAL.AdoNet;
using System;
using System.Data;

namespace MyDAL.Cache
{
    internal class CacheInfo
    {
        public DeserializerState Deserializer { get; set; }

        public Action<IDbCommand, DynamicParameters> ParamReader { get; set; }

    }
}
