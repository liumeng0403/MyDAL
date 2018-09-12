using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.UserFacade.Update
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
