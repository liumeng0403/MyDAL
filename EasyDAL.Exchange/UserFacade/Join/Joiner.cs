using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using System;
using System.Linq.Expressions;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class Joiner : Operator, IMethodObject
    {
        internal Joiner(DbContext dc)
            : base(dc)
        { }

    }
}
