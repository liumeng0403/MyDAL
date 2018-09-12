using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EasyDAL.Exchange.Common;

namespace EasyDAL.Exchange.UserFacade.Delete
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
