using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.MapperX;
using System;
using System.Data;

namespace EasyDAL.Exchange.Cache
{
    internal class CacheInfo
    {
        public DeserializerState Deserializer { get; set; }

        public Action<IDbCommand, DynamicParameters> ParamReader { get; set; }

    }
}
