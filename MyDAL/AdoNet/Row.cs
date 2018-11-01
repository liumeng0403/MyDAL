using System;
using System.Data;

namespace MyDAL.AdoNet
{
    internal struct Row
    {
        internal Func<IDataReader, object> Handle { get; set; }
    }
}
