using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyDAL.AdoNet
{
    internal struct Row
    {
        internal Func<IDataReader, object> Handle { get; set; }
    }
}
