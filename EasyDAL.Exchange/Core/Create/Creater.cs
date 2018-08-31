using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Create
{
    public class Creater<M>: Operator
    {
        internal Creater(DbContext dc)
        {
            DC = dc;
        }
    }
}
