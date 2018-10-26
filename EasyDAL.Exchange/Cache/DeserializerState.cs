using System;
using System.Data;

namespace MyDAL.Cache
{
    internal struct DeserializerState
    {
        public int Hash { get; }
        public Func<IDataReader, object> Func { get; }

        public DeserializerState(int hash, Func<IDataReader, object> func)
        {
            Hash = hash;
            Func = func;
        }
    }
}
