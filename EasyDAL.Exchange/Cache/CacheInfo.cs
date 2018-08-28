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

        public Action<IDbCommand, DynamicParameters> ParamReader { get; set; }

    }
}
