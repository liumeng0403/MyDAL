using System;
using System.Data;

namespace Yunyong.DataExchange.AdoNet
{
    internal interface IRow { }

    internal class Row<M>
        :IRow
    {
        internal Func<IDataReader, M> Handle { get; set; }
    }

    internal class Row
        :IRow
    {
        internal Func<IDataReader, object> Handle { get; set; }
    }
}
