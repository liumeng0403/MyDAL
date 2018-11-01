using System;
using System.Data;

namespace Yunyong.DataExchange.AdoNet
{
    internal struct Row
    {
        internal Func<IDataReader, object> Handle { get; set; }
    }
}
