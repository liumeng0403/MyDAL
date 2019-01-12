using MyDAL.Core.Bases;
using System;
using System.Data;

namespace MyDAL.AdoNet
{

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
