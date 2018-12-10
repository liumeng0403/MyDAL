using System;
using System.Data;

namespace MyDAL.AdoNet
{
    internal interface IRow { }

    internal struct Row<M>
        :IRow
    {
        internal Func<IDataReader, M> Handle { get; set; }
    }

    internal struct Row
        :IRow
    {
        internal Func<IDataReader, object> Handle { get; set; }
    }
}
