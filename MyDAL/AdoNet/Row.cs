using System;
using System.Data;

namespace MyDAL.AdoNet
{
    internal struct Row<M>
    {
        internal Func<IDataReader, M> Handle { get; set; }
    }
}
