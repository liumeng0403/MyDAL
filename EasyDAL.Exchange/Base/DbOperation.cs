using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Base
{
    public abstract partial class DbOperation
    {

        private DbOperation() { }

        public DbOperation(IDbConnection conn)
        {
            AH = AttributeHelper.Instance;
            GH = GenericHelper.Instance;
            EH = ExpressionHelper.Instance;
            DC = new DbContext(conn);
        }


    }
}
