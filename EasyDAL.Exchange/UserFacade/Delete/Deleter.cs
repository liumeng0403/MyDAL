using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Yunyong.DataExchange.Common;

namespace Yunyong.DataExchange.UserFacade.Delete
{
    public class Deleter<M>: Operator,IMethodObject
    {
        internal Deleter(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
        }


    }
}
