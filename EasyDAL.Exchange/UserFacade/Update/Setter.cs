using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Yunyong.DataExchange.UserFacade.Update
{
    public sealed class Setter<M>: Operator,IMethodObject
    {
        internal Setter(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
        }        
    }
}
