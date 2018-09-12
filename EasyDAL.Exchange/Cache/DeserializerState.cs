using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Yunyong.DataExchange.Cache
{
    internal struct DeserializerState
    {
        public readonly int Hash;
        public readonly Func<IDataReader, object> Func;

        public DeserializerState(int hash, Func<IDataReader, object> func)
        {
            Hash = hash;
            Func = func;
        }
    }
}
