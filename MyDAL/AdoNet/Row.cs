using System;
using System.Data;

namespace Yunyong.DataExchange.AdoNet
{
    internal struct Row<M>
    {
        internal Func<IDataReader, M> Handle { get; set; }
    }
}
